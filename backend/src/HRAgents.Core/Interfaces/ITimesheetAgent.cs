namespace HRAgents.Core.Interfaces;

/// <summary>
/// Interface for the timesheet agent that assists with timesheet submissions and corrections.
/// </summary>
public interface ITimesheetAgent
{
    /// <summary>
    /// Gets the agent's instructions/system prompt.
    /// </summary>
    string Instructions { get; }
    
    /// <summary>
    /// Gets the agent's name.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Processes a timesheet-related message asynchronously.
    /// </summary>
    /// <param name="message">The user's message.</param>
    /// <param name="sessionId">The session identifier for context isolation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The agent's response.</returns>
    Task<string> ProcessMessageAsync(
        string message, 
        string sessionId, 
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Processes a timesheet-related message with streaming response.
    /// </summary>
    /// <param name="message">The user's message.</param>
    /// <param name="sessionId">The session identifier for context isolation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Streaming response chunks.</returns>
    IAsyncEnumerable<string> ProcessMessageStreamingAsync(
        string message, 
        string sessionId, 
        CancellationToken cancellationToken = default);
}
