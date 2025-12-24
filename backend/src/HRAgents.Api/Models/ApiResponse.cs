namespace HRAgents.Api.Models;

/// <summary>
/// Standard API response wrapper for consistent error handling.
/// </summary>
/// <typeparam name="T">The type of data being returned.</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Indicates if the request was successful.
    /// </summary>
    public bool Success { get; init; }

    /// <summary>
    /// The response data (null if unsuccessful).
    /// </summary>
    public T? Data { get; init; }

    /// <summary>
    /// Error message if unsuccessful.
    /// </summary>
    public string? Error { get; init; }

    /// <summary>
    /// Detailed error messages (for validation errors).
    /// </summary>
    public Dictionary<string, string[]>? Errors { get; init; }

    /// <summary>
    /// HTTP status code.
    /// </summary>
    public int StatusCode { get; init; }

    /// <summary>
    /// Creates a successful response.
    /// </summary>
    public static ApiResponse<T> Ok(T data)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data,
            StatusCode = 200
        };
    }

    /// <summary>
    /// Creates an error response.
    /// </summary>
    public static ApiResponse<T> Fail(string error, int statusCode = 400)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Error = error,
            StatusCode = statusCode
        };
    }

    /// <summary>
    /// Creates a validation error response.
    /// </summary>
    public static ApiResponse<T> ValidationError(Dictionary<string, string[]> errors)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Error = "Validation failed",
            Errors = errors,
            StatusCode = 400
        };
    }
}
