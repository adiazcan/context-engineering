using HRAgents.Core.Interfaces;

namespace HRAgents.Api.Services;

/// <summary>
/// Service for routing messages to the appropriate agent.
/// </summary>
public interface IAgentRouter
{
    /// <summary>
    /// Routes a message to the specified agent type.
    /// </summary>
    Task<string> RouteMessageAsync(string agentType, string message, string sessionId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Routes a message to the specified agent type with streaming response.
    /// </summary>
    IAsyncEnumerable<string> RouteMessageStreamingAsync(string agentType, string message, string sessionId, CancellationToken cancellationToken = default);
}

/// <summary>
/// Implementation of agent routing logic.
/// </summary>
public class AgentRouter : IAgentRouter
{
    private readonly IVacationAgent _vacationAgent;
    private readonly IProcedureAgent _procedureAgent;
    private readonly ITimesheetAgent _timesheetAgent;

    public AgentRouter(
        IVacationAgent vacationAgent,
        IProcedureAgent procedureAgent,
        ITimesheetAgent timesheetAgent)
    {
        _vacationAgent = vacationAgent ?? throw new ArgumentNullException(nameof(vacationAgent));
        _procedureAgent = procedureAgent ?? throw new ArgumentNullException(nameof(procedureAgent));
        _timesheetAgent = timesheetAgent ?? throw new ArgumentNullException(nameof(timesheetAgent));
    }

    public Task<string> RouteMessageAsync(string agentType, string message, string sessionId, CancellationToken cancellationToken = default)
    {
        return agentType.ToLowerInvariant() switch
        {
            "vacation" => _vacationAgent.ProcessMessageAsync(message, sessionId, cancellationToken),
            "procedure" => _procedureAgent.ProcessMessageAsync(message, sessionId, cancellationToken),
            "timesheet" => _timesheetAgent.ProcessMessageAsync(message, sessionId, cancellationToken),
            _ => throw new ArgumentException($"Unknown agent type: {agentType}", nameof(agentType))
        };
    }

    public IAsyncEnumerable<string> RouteMessageStreamingAsync(string agentType, string message, string sessionId, CancellationToken cancellationToken = default)
    {
        return agentType.ToLowerInvariant() switch
        {
            "vacation" => _vacationAgent.ProcessMessageStreamingAsync(message, sessionId, cancellationToken),
            "procedure" => _procedureAgent.ProcessMessageStreamingAsync(message, sessionId, cancellationToken),
            "timesheet" => _timesheetAgent.ProcessMessageStreamingAsync(message, sessionId, cancellationToken),
            _ => throw new ArgumentException($"Unknown agent type: {agentType}", nameof(agentType))
        };
    }
}
