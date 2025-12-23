namespace HRAgents.Core.Models;

/// <summary>
/// Represents an HR procedure with step-by-step instructions.
/// </summary>
public class HRProcedure
{
    /// <summary>
    /// Gets or sets the unique identifier for the procedure.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the title of the procedure.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the procedure.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of steps in the procedure.
    /// </summary>
    public List<ProcedureStep> Steps { get; set; } = new();

    /// <summary>
    /// Gets or sets any related policies or documents.
    /// </summary>
    public string? RelatedPolicies { get; set; }

    /// <summary>
    /// Gets a value indicating whether this procedure is valid.
    /// </summary>
    public bool IsValid =>
        !string.IsNullOrWhiteSpace(Title) &&
        !string.IsNullOrWhiteSpace(Category) &&
        Steps.Count > 0;
}

/// <summary>
/// Represents a single step in an HR procedure.
/// </summary>
public class ProcedureStep
{
    /// <summary>
    /// Gets or sets the step number.
    /// </summary>
    public int StepNumber { get; set; }

    /// <summary>
    /// Gets or sets the description of the step.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets optional notes for the step.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets a value indicating whether this step is valid.
    /// </summary>
    public bool IsValid => !string.IsNullOrWhiteSpace(Description);
}
