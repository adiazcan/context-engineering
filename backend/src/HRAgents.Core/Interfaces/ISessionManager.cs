using HRAgents.Core.Models;

namespace HRAgents.Core.Interfaces;

/// <summary>
/// Interface for managing agent sessions with isolated state per user.
/// </summary>
public interface ISessionManager
{
    /// <summary>
    /// Gets or creates a session for the specified session ID and user ID.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="userId">The user identifier.</param>
    /// <returns>The agent session.</returns>
    AgentSession GetOrCreateSession(string sessionId, string userId);
    
    /// <summary>
    /// Retrieves an existing session by ID.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <returns>The agent session if found, null otherwise.</returns>
    AgentSession? GetSession(string sessionId);
    
    /// <summary>
    /// Adds a message to a session.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="role">The message role (user or assistant).</param>
    /// <param name="content">The message content.</param>
    /// <param name="agentName">Optional agent name for assistant messages.</param>
    void AddMessage(string sessionId, string role, string content, string? agentName = null);
    
    /// <summary>
    /// Removes a session from the manager.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <returns>True if the session was removed, false otherwise.</returns>
    bool RemoveSession(string sessionId);
    
    /// <summary>
    /// Removes expired sessions based on inactivity timeout.
    /// </summary>
    /// <param name="timeout">The inactivity timeout period.</param>
    /// <returns>The number of sessions removed.</returns>
    int RemoveExpiredSessions(TimeSpan timeout);
}
