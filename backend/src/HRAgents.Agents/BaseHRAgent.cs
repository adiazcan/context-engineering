using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using HRAgents.Core.Interfaces;

namespace HRAgents.Agents;

/// <summary>
/// Base class for all HR agents providing common functionality.
/// </summary>
public abstract class BaseHRAgent
{
    protected readonly IChatClient ChatClient;
    protected readonly ISessionManager SessionManager;
    protected AIAgent Agent { get; }

    protected BaseHRAgent(
        IChatClient chatClient,
        ISessionManager sessionManager,
        string name,
        string instructions)
    {
        ChatClient = chatClient ?? throw new ArgumentNullException(nameof(chatClient));
        SessionManager = sessionManager ?? throw new ArgumentNullException(nameof(sessionManager));
        
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Instructions = instructions ?? throw new ArgumentNullException(nameof(instructions));

        // Create the AI agent using the ChatClientAgent from Microsoft Agent Framework
        Agent = new ChatClientAgent(ChatClient, instructions, name);
    }

    public string Name { get; }
    public string Instructions { get; }

    /// <summary>
    /// Processes a message asynchronously with session isolation.
    /// </summary>
    protected async Task<string> ProcessMessageInternalAsync(
        string message,
        string sessionId,
        CancellationToken cancellationToken = default)
    {
        // Get session-specific chat history
        var session = SessionManager.GetSession(sessionId);
        var messages = BuildChatMessages(session, message);

        // Add user message to session
        SessionManager.AddMessage(sessionId, "user", message);

        // Get agent thread for this session (creates new if doesn't exist)
        var thread = Agent.GetNewThread();

        // Run agent with just the current message (history is in the thread)
        var response = await Agent.RunAsync(message, thread);

        // Extract text from response
        var responseText = response.ToString() ?? string.Empty;

        // Add assistant response to session
        SessionManager.AddMessage(sessionId, "assistant", responseText, Name);

        return responseText;
    }

    /// <summary>
    /// Processes a message with streaming response.
    /// </summary>
    protected async IAsyncEnumerable<string> ProcessMessageStreamingInternalAsync(
        string message,
        string sessionId,
        [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        // Get session-specific chat history
        var session = SessionManager.GetSession(sessionId);

        // Add user message to session
        SessionManager.AddMessage(sessionId, "user", message);

        // Get agent thread for this session
        var thread = Agent.GetNewThread();

        // Stream agent response
        var fullResponse = new List<string>();

        await foreach (var update in Agent.RunStreamingAsync(message, thread))
        {
            var chatUpdate = update.AsChatResponseUpdate();
            
            foreach (var content in chatUpdate.Contents)
            {
                if (content is TextContent textContent && !string.IsNullOrEmpty(textContent.Text))
                {
                    fullResponse.Add(textContent.Text);
                    yield return textContent.Text;
                }
            }
        }

        // Add complete assistant response to session
        if (fullResponse.Count > 0)
        {
            SessionManager.AddMessage(sessionId, "assistant", string.Join("", fullResponse), Name);
        }
    }

    /// <summary>
    /// Builds chat messages including system prompt and session history.
    /// </summary>
    private List<ChatMessage> BuildChatMessages(Core.Models.AgentSession? session, string currentMessage)
    {
        var messages = new List<ChatMessage>
        {
            new(ChatRole.System, Instructions)
        };

        // Add session history if available
        if (session?.Messages != null)
        {
            foreach (var msg in session.Messages)
            {
                var role = msg.Role == "user" ? ChatRole.User : ChatRole.Assistant;
                messages.Add(new ChatMessage(role, msg.Content));
            }
        }

        // Add current message
        messages.Add(new ChatMessage(ChatRole.User, currentMessage));

        return messages;
    }
}
