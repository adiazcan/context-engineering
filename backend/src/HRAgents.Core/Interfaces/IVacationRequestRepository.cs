using HRAgents.Core.Models;

namespace HRAgents.Core.Interfaces;

/// <summary>
/// Repository interface for vacation requests.
/// </summary>
public interface IVacationRequestRepository : IRepository<VacationRequest>
{
    /// <summary>
    /// Gets all vacation requests for a specific employee.
    /// </summary>
    /// <param name="employeeId">The employee ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of vacation requests.</returns>
    Task<IEnumerable<VacationRequest>> GetByEmployeeIdAsync(
        string employeeId, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all vacation requests with a specific status.
    /// </summary>
    /// <param name="status">The status to filter by.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of vacation requests.</returns>
    Task<IEnumerable<VacationRequest>> GetByStatusAsync(
        VacationRequestStatus status, 
        CancellationToken cancellationToken = default);
}
