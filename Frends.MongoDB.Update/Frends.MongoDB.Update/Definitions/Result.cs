namespace Frends.MongoDB.Update.Definitions;

/// <summary>
/// Task result.
/// </summary>
public class Result
{
    /// <summary>
    /// Indicates whether the update completed successfully.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; set; }

    /// <summary>
    /// Number of documents updated.
    /// </summary>
    /// <example>3</example>
    public long Count { get; set; }

    /// <summary>
    /// Error details when the update fails.
    /// </summary>
    /// <example>object { string Message, Exception AdditionalInfo }</example>
    public Error Error { get; set; }
}
