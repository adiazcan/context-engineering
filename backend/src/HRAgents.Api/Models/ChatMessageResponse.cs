namespace HRAgents.Api.Models;

/// <summary>
/// Response model for chat message endpoint.
/// </summary>
public class ChatMessageResponse
{
    /// <summary>
    /// The agent's response message.
    /// </summary>
    public required string Response { get; init; }

    /// <summary>
    /// The session ID used for this conversation.
    /// </summary>
    public required string SessionId { get; init; }

    /// <summary>
    /// The agent that generated the response.
    /// </summary>
    public required string AgentType { get; init; }

    /// <summary>
    /// Timestamp of the response.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
