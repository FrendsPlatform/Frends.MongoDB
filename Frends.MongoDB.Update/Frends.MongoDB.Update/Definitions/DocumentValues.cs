namespace Frends.MongoDB.Update.Definitions;

/// <summary>
/// Input document values.
/// </summary>
public class DocumentValues
{
    /// <summary>
    /// Filter document to process.
    /// </summary>
    /// <example>{'foo':'bar', 'bar':'foo'}</example>
    public string Value { get; set; }
}
