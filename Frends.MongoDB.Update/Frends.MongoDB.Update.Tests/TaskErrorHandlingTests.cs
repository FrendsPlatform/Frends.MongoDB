using Frends.MongoDB.Update;
using Frends.MongoDB.Update.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Frends.MongoDB.Update.Tests;

[TestClass]
public class TaskErrorHandlingTests
{
    [TestMethod]
    public async Task Update_ReturnsStructuredError_WhenThrowingIsDisabled()
    {
        var options = new Options
        {
            ThrowErrorOnFailure = false,
        };

        var result = await MongoDB.Update(new Input(), new Connection(), options, default);

        Assert.IsFalse(result.Success);
        Assert.AreEqual("Update string missing.", result.Error.Message);
        Assert.IsNotNull(result.Error.AdditionalInfo);
    }
}
