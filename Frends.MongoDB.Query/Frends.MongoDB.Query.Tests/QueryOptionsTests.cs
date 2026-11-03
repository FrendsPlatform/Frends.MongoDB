using Frends.MongoDB.Query.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Frends.MongoDB.Query.Tests;

/// <summary>
/// Tests for Query task options that do not require a MongoDB server.
/// </summary>
[TestClass]
public class QueryOptionsTests
{
    /// <summary>
    /// Verifies that an empty filter throws by default.
    /// </summary>
    [TestMethod]
    public void EmptyQueryThrowsByDefault()
    {
        var exception = Assert.ThrowsException<Exception>(
            () => MongoDB.Query(new Input { Filter = string.Empty }, new Connection(), new Options(), default));

        Assert.AreEqual("Query error: Filter can't be null.", exception.Message);
    }

    /// <summary>
    /// Verifies that an empty filter is returned in the result when configured.
    /// </summary>
    [TestMethod]
    public void EmptyQueryReturnsErrorWhenConfigured()
    {
        var options = new Options { ThrowErrorOnFailure = false };

        var result = MongoDB.Query(new Input { Filter = string.Empty }, new Connection(), options, default);

        Assert.IsFalse(result.Success);
        Assert.AreEqual("Query error: Filter can't be null.", result.Error.Message);
        Assert.IsNotNull(result.Error.AdditionalInfo);
    }
}
