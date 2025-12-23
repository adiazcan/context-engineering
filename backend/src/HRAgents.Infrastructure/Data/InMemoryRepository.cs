using System.Collections.Concurrent;
using HRAgents.Core.Interfaces;

namespace HRAgents.Infrastructure.Data;

/// <summary>
/// Base class for in-memory repositories using ConcurrentDictionary for thread-safety.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
public abstract class InMemoryRepository<T> : IRepository<T> where T : class
{
    protected readonly ConcurrentDictionary<string, T> _store = new();

    /// <summary>
    /// Gets the ID property value from an entity.
    /// </summary>
    protected abstract string GetEntityId(T entity);

    /// <summary>
    /// Sets the ID property value for an entity.
    /// </summary>
    protected abstract void SetEntityId(T entity, string id);

    public virtual Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        _store.TryGetValue(id, out var entity);
        return Task.FromResult(entity);
    }

    public virtual Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IEnumerable<T>>(_store.Values.ToList());
    }

    public virtual Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        var id = GetEntityId(entity);
        if (string.IsNullOrEmpty(id))
        {
            id = Guid.NewGuid().ToString();
            SetEntityId(entity, id);
        }

        if (!_store.TryAdd(id, entity))
        {
            throw new InvalidOperationException($"Entity with ID '{id}' already exists.");
        }

        return Task.FromResult(entity);
    }

    public virtual Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        var id = GetEntityId(entity);
        if (!_store.ContainsKey(id))
        {
            throw new InvalidOperationException($"Entity with ID '{id}' not found.");
        }

        _store[id] = entity;
        return Task.FromResult(entity);
    }

    public virtual Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_store.TryRemove(id, out _));
    }

    public virtual Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_store.ContainsKey(id));
    }
}
