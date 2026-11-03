using Frends.MongoDB.Delete.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Frends.MongoDB.Delete.Tests;

[TestClass]
public class UnitTests
{
    /// <summary>
    /// Run command 'docker-compose up -d' in \Frends.MongoDB.Delete.Tests\Files\
    /// </summary>
    private static readonly Connection TestConnection = new()
    {
        ConnectionString = "mongodb://admin:Salakala@localhost:27017/?authSource=admin",
        Database = "testdb",
        CollectionName = "testcoll",
    };

    private static readonly string Doc1 = "{ \"foo\":\"bar\", \"bar\": \"foo\" }";
    private static readonly string Doc2 = "{ \"foo\":\"bar\", \"bar\": \"foo\" }";
    private static readonly string Doc3 = "{ \"qwe\":\"rty\", \"asd\": \"fgh\" }";

    [TestInitialize]
    public void StartUp()
    {
        InsertTestData();
    }

    [TestCleanup]
    public void CleanUp()
    {
        DeleteTestData();
    }

    [TestMethod]
    public async Task Test_Delete_NotFound()
    {
        var input = new Input()
        {
            InputType = InputType.Filter,
            DeleteOptions = Definitions.DeleteOptions.DeleteOne,
            Filter = "{'not':'found'}",
            Filters = null,
            File = null,
        };

        var result = await MongoDB.Delete(input, TestConnection, new Options(), default);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public async Task Test_Delete_Single_DeleteOne()
    {
        var input = new Input()
        {
            InputType = InputType.Filter,
            DeleteOptions = Definitions.DeleteOptions.DeleteOne,
            Filter = "{'foo':'bar'}",
            Filters = null,
            File = null,
        };

        var result = await MongoDB.Delete(input, TestConnection, new Options(), default);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(1, result.Count);
    }

    [TestMethod]
    public async Task Test_Delete_Single_DeleteMany()
    {
        var input = new Input()
        {
            InputType = InputType.Filter,
            DeleteOptions = Definitions.DeleteOptions.DeleteMany,
            Filter = "{'foo':'bar'}",
            Filters = null,
            File = null,
        };

        var result = await MongoDB.Delete(input, TestConnection, new Options(), default);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(2, result.Count);
    }

    [TestMethod]
    public async Task Test_Delete_Array_DeleteOne()
    {
        var doc1 = new DocumentValues() { Value = "{'foo':'bar'}" };
        var doc2 = new DocumentValues() { Value = "{'qwe':'rty'}" };

        var input = new Input()
        {
            InputType = InputType.Filters,
            DeleteOptions = Definitions.DeleteOptions.DeleteOne,
            Filter = null,
            Filters = new[] { doc1, doc2 },
            File = null,
        };

        var result = await MongoDB.Delete(input, TestConnection, new Options(), default);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(2, result.Count);
    }

    [TestMethod]
    public async Task Test_Delete_Array_DeleteMany()
    {
        var doc1 = new DocumentValues() { Value = "{'foo':'bar'}" };
        var doc2 = new DocumentValues() { Value = "{'qwe':'rty'}" };

        var input = new Input()
        {
            InputType = InputType.Filters,
            DeleteOptions = Definitions.DeleteOptions.DeleteMany,
            Filter = null,
            Filters = new[] { doc1, doc2 },
            File = null,
        };

        var result = await MongoDB.Delete(input, TestConnection, new Options(), default);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(3, result.Count);
    }

    [TestMethod]
    public async Task Test_Delete_File_DeleteOne()
    {
        var input = new Input()
        {
            InputType = InputType.File,
            DeleteOptions = Definitions.DeleteOptions.DeleteOne,
            Filter = null,
            Filters = null,
            File = "..//..//..//Files//testdata.json",
        };

        var result = await MongoDB.Delete(input, TestConnection, new Options(), default);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(1, result.Count);
    }

    [TestMethod]
    public async Task Test_Delete_File_DeleteMany()
    {
        var input = new Input()
        {
            InputType = InputType.File,
            DeleteOptions = Definitions.DeleteOptions.DeleteMany,
            Filter = null,
            Filters = null,
            File = "..//..//..//Files//testdata.json",
        };

        var result = await MongoDB.Delete(input, TestConnection, new Options(), default);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(2, result.Count);
    }

    [TestMethod]
    public void Test_InvalidConnectionString()
    {
        var input = new Input()
        {
            InputType = InputType.File,
            DeleteOptions = Definitions.DeleteOptions.DeleteMany,
            Filter = null,
            Filters = null,
            File = "..//..//..//Files//testdata.json",
        };

        var connection = new Connection
        {
            ConnectionString = "mongodb://admin:Incorrect@localhost:27017/?authSource=invalid",
            CollectionName = TestConnection.CollectionName,
            Database = TestConnection.Database,
        };

        var options = new Options { ErrorMessageOnFailure = "Delete error" };
        var ex = Assert.ThrowsExceptionAsync<Exception>(async () => await MongoDB.Delete(input, connection, options, default));
        Assert.AreEqual("Delete error", ex.Result.Message);
    }

    private static void InsertTestData()
    {
        try
        {
            var collection = GetMongoCollection(TestConnection.ConnectionString, TestConnection.Database, TestConnection.CollectionName);

            var document1 = BsonDocument.Parse(Doc1);
            var document2 = BsonDocument.Parse(Doc2);
            var document3 = BsonDocument.Parse(Doc3);

            collection.InsertOne(document1);
            collection.InsertOne(document2);
            collection.InsertOne(document3);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private static void DeleteTestData()
    {
        var collection = GetMongoCollection(TestConnection.ConnectionString, TestConnection.Database, TestConnection.CollectionName);

        var filter1 = "{'bar':'foo'}";
        var filter2 = "{'qwe':'rty'}";
        var filter3 = "{'asd':'fgh'}";
        collection.DeleteMany(filter1);
        collection.DeleteMany(filter2);
        collection.DeleteMany(filter3);
    }

    private static IMongoCollection<BsonDocument> GetMongoCollection(string connectionString, string database, string collectionName)
    {
        var dataBase = GetMongoDatabase(connectionString, database);
        var collection = dataBase.GetCollection<BsonDocument>(collectionName);
        return collection;
    }

    private static IMongoDatabase GetMongoDatabase(string connectionString, string database)
    {
        var mongoClient = new MongoClient(connectionString);
        var dataBase = mongoClient.GetDatabase(database);
        return dataBase;
    }
}
