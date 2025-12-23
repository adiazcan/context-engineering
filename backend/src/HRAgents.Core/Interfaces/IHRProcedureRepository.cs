using HRAgents.Core.Models;

namespace HRAgents.Core.Interfaces;

/// <summary>
/// Repository interface for HR procedures.
/// </summary>
public interface IHRProcedureRepository : IRepository<HRProcedure>
{
    /// <summary>
    /// Gets all procedures in a specific category.
    /// </summary>
    /// <param name="category">The category to filter by.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of HR procedures.</returns>
    Task<IEnumerable<HRProcedure>> GetByCategoryAsync(
        string category, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches procedures by title or content.
    /// </summary>
    /// <param name="searchTerm">The search term.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of matching HR procedures.</returns>
    Task<IEnumerable<HRProcedure>> SearchAsync(
        string searchTerm, 
        CancellationToken cancellationToken = default);
}
