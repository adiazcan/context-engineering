using System.ComponentModel.DataAnnotations;

namespace HRAgents.Api.Models;

/// <summary>
/// Request model for sending a chat message to an agent.
/// </summary>
public class ChatMessageRequest
{
    /// <summary>
    /// The message content from the user.
    /// </summary>
    [Required(ErrorMessage = "Message is required")]
    [StringLength(4000, MinimumLength = 1, ErrorMessage = "Message must be between 1 and 4000 characters")]
    public required string Message { get; init; }

    /// <summary>
    /// The session ID for conversation context.
    /// </summary>
    [Required(ErrorMessage = "SessionId is required")]
    public required string SessionId { get; init; }

    /// <summary>
    /// The target agent (vacation, procedure, or timesheet).
    /// </summary>
    [Required(ErrorMessage = "AgentType is required")]
    [RegularExpression("^(vacation|procedure|timesheet)$", 
        ErrorMessage = "AgentType must be 'vacation', 'procedure', or 'timesheet'")]
    public required string AgentType { get; init; }

    /// <summary>
    /// Optional user ID for authentication context.
    /// </summary>
    public string? UserId { get; init; }
}
