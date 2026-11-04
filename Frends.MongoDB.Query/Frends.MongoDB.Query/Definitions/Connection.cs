using System.ComponentModel;

namespace Frends.MongoDB.Query.Definitions;

/// <summary>
/// Connection parameters.
/// </summary>
public class Connection
{
    /// <summary>
    /// Gets or sets the MongoDB connection string.
    /// </summary>
    /// <example>mongodb://localhost:27017/?authSource=admin&amp;appName=Frends.MongoDB.Query.</example>
    [PasswordPropertyText]
    public string ConnectionString { get; set; }

    /// <summary>
    /// Name of the database to connect to
    /// </summary>
    /// <example>analytics</example>
    public string Database { get; set; }

    /// <summary>
    /// Name of the collection name.
    /// </summary>
    /// <example>customers</example>
    public string CollectionName { get; set; }
}
