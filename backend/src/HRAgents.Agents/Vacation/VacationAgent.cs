using Microsoft.Extensions.AI;
using HRAgents.Core.Interfaces;

namespace HRAgents.Agents.Vacation;

/// <summary>
/// Agent responsible for handling vacation requests and approvals.
/// </summary>
public class VacationAgent : BaseHRAgent, IVacationAgent
{
    private const string AgentInstructions = @"You are a helpful HR vacation assistant. 
Your responsibilities include:
- Helping employees submit vacation requests
- Checking the status of vacation requests
- Providing information about vacation policies
- Assisting with vacation request approvals (for managers)

Be professional, friendly, and efficient. Always ask for missing information politely.
When handling vacation requests, ensure you collect: start date, end date, and reason.";

    public VacationAgent(IChatClient chatClient, ISessionManager sessionManager)
        : base(chatClient, sessionManager, "VacationAgent", AgentInstructions)
    {
    }

    public Task<string> ProcessMessageAsync(
        string message, 
        string sessionId, 
        CancellationToken cancellationToken = default)
    {
        return ProcessMessageInternalAsync(message, sessionId, cancellationToken);
    }

    public IAsyncEnumerable<string> ProcessMessageStreamingAsync(
        string message, 
        string sessionId, 
        CancellationToken cancellationToken = default)
    {
        return ProcessMessageStreamingInternalAsync(message, sessionId, cancellationToken);
    }
}
