namespace Frends.MongoDB.Query.Definitions;

/// <summary>
/// Error that occurred during the task.
/// </summary>
public class Error
{
    /// <summary>
    /// Gets or sets the error summary.
    /// </summary>
    /// <example>Query failed because MongoDB rejected the authentication credentials.</example>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets additional information about the error.
    /// </summary>
    /// <example>MongoAuthenticationException: Unable to authenticate using SCRAM-SHA-1.</example>
    public Exception AdditionalInfo { get; set; }
}
