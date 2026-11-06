using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.MongoDB.Query.Definitions;

/// <summary>
/// Additional parameters.
/// </summary>
public class Options
{
    /// <summary>
    /// Gets or sets a value indicating whether to throw an error when the query fails.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool ThrowErrorOnFailure { get; set; } = true;

    /// <summary>
    /// Gets or sets the error message to use when the query fails.
    /// </summary>
    /// <example>MongoDB query failed due to invalid credentials.</example>
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue("")]
    public string ErrorMessageOnFailure { get; set; } = string.Empty;
}
