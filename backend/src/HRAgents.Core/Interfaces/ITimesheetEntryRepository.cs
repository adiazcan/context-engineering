using HRAgents.Core.Models;

namespace HRAgents.Core.Interfaces;

/// <summary>
/// Repository interface for timesheet entries.
/// </summary>
public interface ITimesheetEntryRepository : IRepository<TimesheetEntry>
{
    /// <summary>
    /// Gets all timesheet entries for a specific employee.
    /// </summary>
    /// <param name="employeeId">The employee ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of timesheet entries.</returns>
    Task<IEnumerable<TimesheetEntry>> GetByEmployeeIdAsync(
        string employeeId, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all timesheet entries for a specific date range.
    /// </summary>
    /// <param name="employeeId">The employee ID.</param>
    /// <param name="startDate">The start date.</param>
    /// <param name="endDate">The end date.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of timesheet entries.</returns>
    Task<IEnumerable<TimesheetEntry>> GetByDateRangeAsync(
        string employeeId,
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all timesheet entries with a specific status.
    /// </summary>
    /// <param name="status">The status to filter by.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of timesheet entries.</returns>
    Task<IEnumerable<TimesheetEntry>> GetByStatusAsync(
        TimesheetStatus status, 
        CancellationToken cancellationToken = default);
}
