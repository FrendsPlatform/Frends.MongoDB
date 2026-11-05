namespace Frends.MongoDB.Query.Definitions;

/// <summary>
/// Task's result.
/// </summary>
public class Result
{
    /// <summary>
    /// Indicates if the task completed successfully.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; set; }

    /// <summary>
    /// A list with the documents matching the search criteria .
    /// </summary>
    /// <example>"Object{ \"_id\" : { \"$oid\" : \"635b6e17083070195c6e98f0\" }, \"foo\" : \"bar\", \"bar\" : \"foo\" }"</example>
    public List<string> Data { get; set; }

    /// <summary>
    /// Error that occurred during task execution.
    /// </summary>
    /// <example>object { string Message, Exception AdditionalInfo }</example>
    public Error Error { get; set; }
}
