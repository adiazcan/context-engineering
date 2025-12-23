using HRAgents.Core.Interfaces;
using HRAgents.Core.Models;

namespace HRAgents.Infrastructure.Data;

/// <summary>
/// In-memory repository for HR procedures.
/// </summary>
public class InMemoryHRProcedureRepository : InMemoryRepository<HRProcedure>, IHRProcedureRepository
{
    protected override string GetEntityId(HRProcedure entity) => entity.Id;

    protected override void SetEntityId(HRProcedure entity, string id) => entity.Id = id;

    public Task<IEnumerable<HRProcedure>> GetByCategoryAsync(
        string category, 
        CancellationToken cancellationToken = default)
    {
        var procedures = _store.Values
            .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            .ToList();
        return Task.FromResult<IEnumerable<HRProcedure>>(procedures);
    }

    public Task<IEnumerable<HRProcedure>> SearchAsync(
        string searchTerm, 
        CancellationToken cancellationToken = default)
    {
        var lowerSearchTerm = searchTerm.ToLowerInvariant();
        var procedures = _store.Values
            .Where(p => 
                p.Title.ToLowerInvariant().Contains(lowerSearchTerm) ||
                p.Category.ToLowerInvariant().Contains(lowerSearchTerm) ||
                p.Steps.Any(s => s.Description.ToLowerInvariant().Contains(lowerSearchTerm)))
            .ToList();
        return Task.FromResult<IEnumerable<HRProcedure>>(procedures);
    }
}
