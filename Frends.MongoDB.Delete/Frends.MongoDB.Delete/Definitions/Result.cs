namespace Frends.MongoDB.Delete.Definitions;

/// <summary>
/// Task's result.
/// </summary>
public class Result
{
    internal Result(bool success, long count, Error error = null)
    {
        Success = success;
        Count = count;
        Error = error;
    }

    /// <summary>
    /// Delete complete.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; private set; }

    /// <summary>
    /// Count of deleted documents.
    /// </summary>
    /// <example>1</example>
    public long Count { get; private set; }

    /// <summary>
    /// Error that occurred during task execution.
    /// </summary>
    /// <example>null</example>
    public Error Error { get; private set; }
}
