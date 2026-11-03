using System;

namespace Frends.MongoDB.Insert.Definitions;

/// <summary>
/// Error that occurred during the task.
/// </summary>
public class Error
{
    /// <summary>
    /// Summary of the error.
    /// </summary>
    /// <example>MongoDB rejected the document because its _id already exists.</example>
    public string Message { get; set; }

    /// <summary>
    /// Additional information about the error.
    /// </summary>
    /// <example>MongoDB.Driver.MongoWriteException: duplicate key</example>
    public Exception AdditionalInfo { get; set; }
}
