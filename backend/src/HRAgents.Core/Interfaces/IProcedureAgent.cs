namespace HRAgents.Core.Interfaces;

/// <summary>
/// Interface for the procedure agent that provides guidance on HR procedures.
/// </summary>
public interface IProcedureAgent
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
    /// Processes a procedure-related message asynchronously.
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
    /// Processes a procedure-related message with streaming response.
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
