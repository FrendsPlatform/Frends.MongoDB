using System;

namespace Frends.MongoDB.Update.Definitions;

/// <summary>
/// Error that occurred during the task.
/// </summary>
public class Error
{
    /// <summary>
    /// Summary of the error.
    /// </summary>
    /// <example>MongoDB update failed: invalid filter</example>
    public string Message { get; set; }

    /// <summary>
    /// Additional information about the error.
    /// </summary>
    /// <example>MongoDB.Driver.MongoException details</example>
    public Exception AdditionalInfo { get; set; }
}
