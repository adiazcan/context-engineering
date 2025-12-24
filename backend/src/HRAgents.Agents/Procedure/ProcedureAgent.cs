using Microsoft.Extensions.AI;
using HRAgents.Core.Interfaces;

namespace HRAgents.Agents.Procedure;

/// <summary>
/// Agent responsible for providing guidance on HR procedures.
/// </summary>
public class ProcedureAgent : BaseHRAgent, IProcedureAgent
{
    private const string AgentInstructions = @"You are a knowledgeable HR procedures assistant.
Your responsibilities include:
- Helping employees understand HR procedures and policies
- Providing step-by-step guidance through HR processes
- Answering questions about HR policies
- Recommending appropriate procedures based on employee needs

Be clear, concise, and helpful. Break down complex procedures into simple steps.";

    public ProcedureAgent(IChatClient chatClient, ISessionManager sessionManager)
        : base(chatClient, sessionManager, "ProcedureAgent", AgentInstructions)
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
