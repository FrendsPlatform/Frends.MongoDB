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
    /// Gets or sets the database name.
    /// </summary>
    /// <example>analytics, the database used for reporting.</example>
    public string Database { get; set; }

    /// <summary>
    /// Gets or sets the collection name.
    /// </summary>
    /// <example>customers, the collection containing customer documents.</example>
    public string CollectionName { get; set; }
}
