using System.Collections.Concurrent;
using HRAgents.Core.Interfaces;
using HRAgents.Core.Models;

namespace HRAgents.Infrastructure.Services;

/// <summary>
/// Thread-safe in-memory implementation of session manager.
/// </summary>
public class InMemorySessionManager : ISessionManager
{
    private readonly ConcurrentDictionary<string, AgentSession> _sessions = new();

    public AgentSession GetOrCreateSession(string sessionId, string userId)
    {
        return _sessions.GetOrAdd(sessionId, _ => new AgentSession
        {
            SessionId = sessionId,
            UserId = userId
        });
    }

    public AgentSession? GetSession(string sessionId)
    {
        _sessions.TryGetValue(sessionId, out var session);
        return session;
    }

    public void AddMessage(string sessionId, string role, string content, string? agentName = null)
    {
        if (_sessions.TryGetValue(sessionId, out var session))
        {
            session.Messages.Add(new SessionMessage
            {
                Role = role,
                Content = content,
                AgentName = agentName
            });
            session.LastActivityAt = DateTime.UtcNow;
        }
    }

    public bool RemoveSession(string sessionId)
    {
        return _sessions.TryRemove(sessionId, out _);
    }

    public int RemoveExpiredSessions(TimeSpan timeout)
    {
        var expiredSessions = _sessions
            .Where(kvp => DateTime.UtcNow - kvp.Value.LastActivityAt > timeout)
            .Select(kvp => kvp.Key)
            .ToList();

        var removedCount = 0;
        foreach (var sessionId in expiredSessions)
        {
            if (RemoveSession(sessionId))
            {
                removedCount++;
            }
        }

        return removedCount;
    }
}
