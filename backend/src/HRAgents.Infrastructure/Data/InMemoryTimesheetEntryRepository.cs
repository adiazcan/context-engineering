using HRAgents.Core.Interfaces;
using HRAgents.Core.Models;

namespace HRAgents.Infrastructure.Data;

/// <summary>
/// In-memory repository for timesheet entries.
/// </summary>
public class InMemoryTimesheetEntryRepository : InMemoryRepository<TimesheetEntry>, ITimesheetEntryRepository
{
    protected override string GetEntityId(TimesheetEntry entity) => entity.Id;

    protected override void SetEntityId(TimesheetEntry entity, string id) => entity.Id = id;

    public Task<IEnumerable<TimesheetEntry>> GetByEmployeeIdAsync(
        string employeeId, 
        CancellationToken cancellationToken = default)
    {
        var entries = _store.Values
            .Where(e => e.EmployeeId == employeeId)
            .ToList();
        return Task.FromResult<IEnumerable<TimesheetEntry>>(entries);
    }

    public Task<IEnumerable<TimesheetEntry>> GetByDateRangeAsync(
        string employeeId,
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken = default)
    {
        var entries = _store.Values
            .Where(e => e.EmployeeId == employeeId && 
                       e.Date >= startDate && 
                       e.Date <= endDate)
            .ToList();
        return Task.FromResult<IEnumerable<TimesheetEntry>>(entries);
    }

    public Task<IEnumerable<TimesheetEntry>> GetByStatusAsync(
        TimesheetStatus status, 
        CancellationToken cancellationToken = default)
    {
        var entries = _store.Values
            .Where(e => e.Status == status)
            .ToList();
        return Task.FromResult<IEnumerable<TimesheetEntry>>(entries);
    }
}
