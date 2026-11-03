using System;

namespace Frends.MongoDB.Delete.Definitions;

/// <summary>
/// Error that occurred during the task.
/// </summary>
public class Error
{
    /// <summary>
    /// Summary of the error.
    /// </summary>
    /// <example>Unable to delete documents from MongoDB.</example>
    public string Message { get; set; }

    /// <summary>
    /// Additional information about the error.
    /// </summary>
    /// <example>Unable to authenticate while connecting to MongoDB.</example>
    public Exception AdditionalInfo { get; set; }
}
