namespace Frends.MongoDB.Query.Definitions;

/// <summary>
/// Task's result.
/// </summary>
public class Result
{
    /// <summary>
    /// Gets or sets a value indicating whether the query completed successfully.
    /// </summary>
    /// <example>true, indicating the query completed successfully.</example>
    public bool Success { get; set; }

    /// <summary>
    /// A list with the documents matching the search criteria .
    /// </summary>
    /// <example>"Object{ \"_id\" : { \"$oid\" : \"635b6e17083070195c6e98f0\" }, \"foo\" : \"bar\", \"bar\" : \"foo\" }"</example>
    public List<string> Data { get; set; }

    /// <summary>
    /// Gets or sets error details when the query fails.
    /// </summary>
    /// <example>null when the query succeeds.</example>
    public Error Error { get; set; }
}
