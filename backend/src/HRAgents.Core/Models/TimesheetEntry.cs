namespace HRAgents.Core.Models;

/// <summary>
/// Represents a timesheet entry for an employee.
/// </summary>
public class TimesheetEntry
{
    /// <summary>
    /// Gets or sets the unique identifier for the timesheet entry.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the employee ID.
    /// </summary>
    public string EmployeeId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date for this timesheet entry.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the number of hours worked.
    /// </summary>
    public decimal Hours { get; set; }

    /// <summary>
    /// Gets or sets the project code.
    /// </summary>
    public string ProjectCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the optional description of work performed.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the current status of the timesheet entry.
    /// </summary>
    public TimesheetStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the entry was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets a value indicating whether this timesheet entry is valid.
    /// </summary>
    public bool IsValid => 
        Hours > 0 && 
        Hours <= 24 && 
        Date <= DateTime.Today &&
        !string.IsNullOrWhiteSpace(ProjectCode);

    /// <summary>
    /// Submits the timesheet entry for approval.
    /// </summary>
    public void Submit()
    {
        if (Status == TimesheetStatus.Draft)
        {
            Status = TimesheetStatus.Submitted;
        }
    }
}

/// <summary>
/// Represents the status of a timesheet entry.
/// </summary>
public enum TimesheetStatus
{
    Draft,
    Submitted,
    Approved,
    Rejected
}
