using HRAgents.Core.Interfaces;
using HRAgents.Core.Models;

namespace HRAgents.Infrastructure.Data;

/// <summary>
/// In-memory repository for vacation requests.
/// </summary>
public class InMemoryVacationRequestRepository : InMemoryRepository<VacationRequest>, IVacationRequestRepository
{
    protected override string GetEntityId(VacationRequest entity) => entity.Id;

    protected override void SetEntityId(VacationRequest entity, string id) => entity.Id = id;

    public Task<IEnumerable<VacationRequest>> GetByEmployeeIdAsync(
        string employeeId, 
        CancellationToken cancellationToken = default)
    {
        var requests = _store.Values
            .Where(r => r.EmployeeId == employeeId)
            .ToList();
        return Task.FromResult<IEnumerable<VacationRequest>>(requests);
    }

    public Task<IEnumerable<VacationRequest>> GetByStatusAsync(
        VacationRequestStatus status, 
        CancellationToken cancellationToken = default)
    {
        var requests = _store.Values
            .Where(r => r.Status == status)
            .ToList();
        return Task.FromResult<IEnumerable<VacationRequest>>(requests);
    }
}
