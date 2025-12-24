namespace HRAgents.Api.Models;

/// <summary>
/// Response model for chat history endpoint.
/// </summary>
public class ChatHistoryResponse
{
    /// <summary>
    /// The session ID.
    /// </summary>
    public required string SessionId { get; init; }

    /// <summary>
    /// The user ID associated with the session.
    /// </summary>
    public required string UserId { get; init; }

    /// <summary>
    /// List of messages in the conversation.
    /// </summary>
    public required List<ChatHistoryMessage> Messages { get; init; }

    /// <summary>
    /// When the session was created.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Last activity timestamp.
    /// </summary>
    public DateTime LastActivityAt { get; init; }
}

/// <summary>
/// Represents a message in the chat history.
/// </summary>
public class ChatHistoryMessage
{
    /// <summary>
    /// The role of the message sender (user or assistant).
    /// </summary>
    public required string Role { get; init; }

    /// <summary>
    /// The message content.
    /// </summary>
    public required string Content { get; init; }

    /// <summary>
    /// When the message was sent.
    /// </summary>
    public DateTime Timestamp { get; init; }

    /// <summary>
    /// The agent name if this is an assistant message.
    /// </summary>
    public string? AgentName { get; init; }
}
