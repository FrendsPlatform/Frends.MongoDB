using Frends.MongoDB.Insert.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Frends.MongoDB.Insert.Tests;

[TestClass]
public class UnitTests
{
    /*
        Run command 'docker-compose up -d' in \Frends.MongoDB.Insert.Tests\Files\
    */

    private static readonly Connection MongoConnection = new()
    {
        ConnectionString = "mongodb://admin:Salakala@localhost:27017/?authSource=admin",
        Database = "testdb",
        CollectionName = "testcoll",
    };

    private readonly string doc1 = "{ \"foo\":\"bar\", \"bar\": \"foo\" }";
    private readonly string doc2 = "{ \"foo\":\"bar2\", \"bar\": \"foo2\" }";

    [TestMethod]
    public async Task Test_Insert_Document()
    {
        var input = new Input()
        {
            InputType = InputType.Document,
            Document = doc1,
            File = null,
        };

        var result = await MongoDB.Insert(input, MongoConnection, new Options(), default);
        Assert.IsTrue(result.Success.Equals(true) && result.Id != null);
    }

    [TestMethod]
    public async Task Test_Insert_Documents()
    {
        var document1 = new DocumentValues() { Value = doc1 };
        var document2 = new DocumentValues() { Value = doc2 };

        var input = new Input()
        {
            InputType = InputType.Documents,
            Documents = new[] { document1, document2 },
            Document = null,
            File = null,
        };

        var result = await MongoDB.Insert(input, MongoConnection, new Options(), default);
        Assert.IsTrue(result.Success.Equals(true) && result.Id != null);
    }

    [TestMethod]
    public async Task Test_Insert_File()
    {
        var input = new Input()
        {
            InputType = InputType.File,
            Document = null,
            File = "..//..//..//Files//testdata.json",
        };

        var result = await MongoDB.Insert(input, MongoConnection, new Options(), default);
        Assert.IsTrue(result.Success.Equals(true) && result.Id != null);
    }

    [TestMethod]
    public async Task Test_Insert_Returns_Error_When_ThrowErrorOnFailure_Is_False()
    {
        var input = new Input
        {
            InputType = InputType.Document,
            Document = "not json",
        };
        var testConnection = new Connection
        {
            ConnectionString = "mongodb://localhost:27017",
            Database = "testdb",
            CollectionName = "testcoll",
        };
        var options = new Options { ThrowErrorOnFailure = false };

        var result = await MongoDB.Insert(input, testConnection, options, default);

        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Error);
        Assert.IsNotNull(result.Error.AdditionalInfo);
    }
}
