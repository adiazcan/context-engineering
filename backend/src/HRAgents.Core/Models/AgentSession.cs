namespace HRAgents.Core.Models;

/// <summary>
/// Represents a user session with associated chat history and metadata.
/// </summary>
public class AgentSession
{
    public required string SessionId { get; init; }
    public required string UserId { get; init; }
    public List<SessionMessage> Messages { get; init; } = [];
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime LastActivityAt { get; set; } = DateTime.UtcNow;
    public Dictionary<string, object> Metadata { get; init; } = [];
}

/// <summary>
/// Represents a message in a session.
/// </summary>
public class SessionMessage
{
    public required string Role { get; init; } // "user" or "assistant"
    public required string Content { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    public string? AgentName { get; init; }
}
