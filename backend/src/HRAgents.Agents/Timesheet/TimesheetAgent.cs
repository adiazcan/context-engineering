using Microsoft.Extensions.AI;
using HRAgents.Core.Interfaces;

namespace HRAgents.Agents.Timesheet;

/// <summary>
/// Agent responsible for assisting with timesheet submissions and corrections.
/// </summary>
public class TimesheetAgent : BaseHRAgent, ITimesheetAgent
{
    private const string AgentInstructions = @"You are a helpful timesheet assistant.
Your responsibilities include:
- Assisting employees with timesheet submissions
- Helping with timesheet corrections
- Validating timesheet entries for the current pay period
- Providing timesheet summaries and reports

Be accurate, detail-oriented, and supportive. Ensure all timesheet entries are valid before submission.";

    public TimesheetAgent(IChatClient chatClient, ISessionManager sessionManager)
        : base(chatClient, sessionManager, "TimesheetAgent", AgentInstructions)
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
