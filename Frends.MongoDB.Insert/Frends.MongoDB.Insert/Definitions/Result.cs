using System.Collections.Generic;

namespace Frends.MongoDB.Insert.Definitions;

/// <summary>
/// Task's result.
/// </summary>
public class Result
{
    internal Result(bool success, List<string> id, Error error = null)
    {
        Success = success;
        Id = id;
        Error = error;
    }

    /// <summary>
    /// Document(s) added.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; private set; }

    /// <summary>
    /// List of ID(s) of the added document(s).
    /// </summary>
    /// <example>6357ae6e8b77e824c8381113</example>
    public List<string> Id { get; private set; }

    /// <summary>
    /// Error that occurred during task execution.
    /// </summary>
    /// <example>MongoDB insert failed: document validation error</example>
    public Error Error { get; private set; }
}
