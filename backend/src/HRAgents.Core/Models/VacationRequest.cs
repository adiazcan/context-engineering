namespace HRAgents.Core.Models;

/// <summary>
/// Represents a vacation request submitted by an employee.
/// </summary>
public class VacationRequest
{
    /// <summary>
    /// Gets or sets the unique identifier for the vacation request.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the employee ID who submitted the request.
    /// </summary>
    public string EmployeeId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the start date of the vacation.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the vacation.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the reason for the vacation.
    /// </summary>
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the current status of the request.
    /// </summary>
    public VacationRequestStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the ID of the person who approved or rejected the request.
    /// </summary>
    public string? ApprovedBy { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the request was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the request was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets the total number of vacation days (inclusive).
    /// </summary>
    public int TotalDays => (EndDate - StartDate).Days + 1;

    /// <summary>
    /// Gets a value indicating whether this vacation request is valid.
    /// </summary>
    public bool IsValid => EndDate >= StartDate;

    /// <summary>
    /// Approves the vacation request.
    /// </summary>
    /// <param name="approverDid">The ID of the approver.</param>
    public void Approve(string approverId)
    {
        Status = VacationRequestStatus.Approved;
        ApprovedBy = approverId;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Rejects the vacation request.
    /// </summary>
    /// <param name="approverId">The ID of the person rejecting the request.</param>
    public void Reject(string approverId)
    {
        Status = VacationRequestStatus.Rejected;
        ApprovedBy = approverId;
        UpdatedAt = DateTime.UtcNow;
    }
}

/// <summary>
/// Represents the status of a vacation request.
/// </summary>
public enum VacationRequestStatus
{
    Pending,
    Approved,
    Rejected,
    Cancelled
}
