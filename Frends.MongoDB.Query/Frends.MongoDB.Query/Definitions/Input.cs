namespace Frends.MongoDB.Query.Definitions;

/// <summary>
/// Input parameter.
/// </summary>
public class Input
{
    /// <summary>
    /// Gets or sets the filter document.
    /// </summary>
    /// <example>Filter documents with an active status: {'status':'active'}.</example>
    public string Filter { get; set; }
}
