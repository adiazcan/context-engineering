using Microsoft.Extensions.AI;
using System.Runtime.CompilerServices;

namespace HRAgents.Api.Services;

/// <summary>
/// Mock implementation of IChatClient for development without actual LLM.
/// Returns simple echo responses.
/// </summary>
public class MockChatClient : IChatClient
{
    public ChatClientMetadata Metadata => new("MockChatClient", new Uri("https://localhost"), "mock-model");

    public Task<ChatResponse> GetResponseAsync(
        IEnumerable<ChatMessage> chatMessages,
        ChatOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        var messages = chatMessages.ToList();
        var lastMessage = messages.LastOrDefault()?.Text ?? "No message";
        var systemPrompt = messages.FirstOrDefault(m => m.Role == ChatRole.System)?.Text ?? "";

        // Simple mock response based on content
        var responseText = GenerateMockResponse(lastMessage, systemPrompt);

        var response = new ChatResponse(new[]
        {
            new ChatMessage(ChatRole.Assistant, responseText)
        })
        {
            FinishReason = ChatFinishReason.Stop
        };

        return Task.FromResult(response);
    }

    public async IAsyncEnumerable<ChatResponseUpdate> GetStreamingResponseAsync(
        IEnumerable<ChatMessage> chatMessages,
        ChatOptions? options = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var messages = chatMessages.ToList();
        var lastMessage = messages.LastOrDefault()?.Text ?? "No message";
        var systemPrompt = messages.FirstOrDefault(m => m.Role == ChatRole.System)?.Text ?? "";

        var responseText = GenerateMockResponse(lastMessage, systemPrompt);

        // Simulate streaming by breaking response into chunks
        var words = responseText.Split(' ');
        foreach (var word in words)
        {
            if (cancellationToken.IsCancellationRequested)
                break;

            yield return new ChatResponseUpdate
            {
                Contents = [new TextContent(word + " ")],
                Role = ChatRole.Assistant
            };

            // Simulate network delay
            await Task.Delay(50, cancellationToken);
        }

        // Final chunk with finish reason
        yield return new ChatResponseUpdate
        {
            FinishReason = ChatFinishReason.Stop
        };
    }

    private static string GenerateMockResponse(string userMessage, string systemPrompt)
    {
        var lowerMessage = userMessage.ToLowerInvariant();

        // Vacation agent responses
        if (systemPrompt.Contains("vacation", StringComparison.OrdinalIgnoreCase))
        {
            if (lowerMessage.Contains("request") || lowerMessage.Contains("time off"))
                return "I can help you submit a vacation request. Could you please provide the start date, end date, and reason for your time off?";
            if (lowerMessage.Contains("status"))
                return "To check your vacation request status, I'll need your employee ID. Your pending requests will show their current approval status.";
            if (lowerMessage.Contains("policy"))
                return "Our vacation policy allows employees to accrue PTO based on years of service. Full-time employees receive 15 days per year to start.";
            return "I'm your vacation assistant. I can help you submit vacation requests, check request status, or answer questions about our vacation policies.";
        }

        // Procedure agent responses
        if (systemPrompt.Contains("procedure", StringComparison.OrdinalIgnoreCase))
        {
            if (lowerMessage.Contains("onboard") || lowerMessage.Contains("new hire"))
                return "The onboarding process involves: 1) Complete I-9 verification, 2) Review and sign employee handbook, 3) Set up benefits enrollment, 4) Complete required training modules, 5) Meet with your manager for orientation.";
            if (lowerMessage.Contains("benefit") || lowerMessage.Contains("insurance"))
                return "To enroll in benefits, log into the HR portal within 30 days of your start date. You can select health, dental, vision insurance, and 401(k) options. Need help with a specific benefit?";
            if (lowerMessage.Contains("expense") || lowerMessage.Contains("reimburse"))
                return "To submit an expense report: 1) Log into the expense system, 2) Upload receipts, 3) Categorize expenses, 4) Submit for manager approval. Reimbursement typically processes within 5-7 business days.";
            return "I'm your HR procedures assistant. I can guide you through onboarding, benefits enrollment, expense reimbursement, and other HR processes.";
        }

        // Timesheet agent responses
        if (systemPrompt.Contains("timesheet", StringComparison.OrdinalIgnoreCase))
        {
            if (lowerMessage.Contains("submit") || lowerMessage.Contains("enter"))
                return "To submit your timesheet, please enter your hours worked for each day of the current pay period. Make sure to include your project code and any notes about the work performed.";
            if (lowerMessage.Contains("correct") || lowerMessage.Contains("fix") || lowerMessage.Contains("change"))
                return "To correct a submitted timesheet, you'll need to contact your manager for approval first. Once approved, I can help you make the corrections. What needs to be changed?";
            if (lowerMessage.Contains("period"))
                return "The current pay period runs bi-weekly from Monday to Sunday. Timesheets are due by 5 PM on the Monday following the end of the pay period.";
            return "I'm your timesheet assistant. I can help you submit timesheets, make corrections, and answer questions about pay periods and time tracking.";
        }

        return $"I received your message: '{userMessage}'. This is a mock response for development purposes.";
    }

    public void Dispose()
    {
        // No resources to dispose
    }

    public object? GetService(Type serviceType, object? key = null)
    {
        return null;
    }
}
