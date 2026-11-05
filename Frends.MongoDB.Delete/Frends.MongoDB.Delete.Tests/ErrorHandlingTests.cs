using Frends.MongoDB.Delete.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Frends.MongoDB.Delete.Tests;

[TestClass]
public class ErrorHandlingTests
{
    [TestMethod]
    public async Task InvalidFilter_ReturnsErrorWhenThrowDisabled()
    {
        var input = new Input
        {
            InputType = InputType.Filter,
            DeleteOptions = Definitions.DeleteOptions.DeleteOne,
            Filter = "{invalid",
        };
        var connection = new Connection
        {
            ConnectionString = "mongodb://localhost:27017",
            Database = "testdb",
            CollectionName = "testcoll",
        };

        var result = await MongoDB.Delete(input, connection, new Options { ThrowErrorOnFailure = false }, default);

        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Error);
        Assert.IsNotNull(result.Error.Message);
        Assert.IsNotNull(result.Error.AdditionalInfo);
    }
}
