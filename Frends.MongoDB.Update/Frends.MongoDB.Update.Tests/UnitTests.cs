using Frends.MongoDB.Update.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Frends.MongoDB.Update.Tests;

[TestClass]
public class UnitTests
{
    /// <summary>
    /// Run command 'docker-compose up -d' in \Frends.MongoDB.Update.Tests\Files\
    /// </summary>
    private static readonly Connection MongoConnection = new()
    {
        ConnectionString = "mongodb://admin:Salakala@localhost:27017/?authSource=admin",
        Database = "testdb",
        CollectionName = "testcoll",
    };

    private readonly string document1Json = "{ 'foo':'bar', 'bar': 'foo' }";
    private readonly string document2Json = "{ 'foo':'bar', 'bar': 'foo' }";
    private readonly string document3Json = "{ 'qwe':'rty', 'asd': 'fgh' }";

    [TestInitialize]
    public void StartUp()
    {
        InsertTestData();
    }

    [TestCleanup]
    public void CleanUp()
    {
        UpdateTestData();
    }

    [TestMethod]
    public async Task Test_Update_NotFound()
    {
        var input = new Input()
        {
            InputType = InputType.Filter,
            UpdateOptions = Definitions.UpdateOptions.UpdateOne,
            Filter = "{'not':'found'}",
            Filters = null,
            File = null,
            UpdateString = "{$set: {foo:'update'}}",
        };

        var result = await MongoDB.Update(input, MongoConnection, new Options(), default);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(0, result.Count);
        Assert.IsFalse(GetDocuments("update"));
    }

    [TestMethod]
    public async Task Test_Update_Single_UpdateOne()
    {
        var input = new Input()
        {
            InputType = InputType.Filter,
            UpdateOptions = Definitions.UpdateOptions.UpdateOne,
            Filter = "{'foo':'bar'}",
            Filters = null,
            File = null,
            UpdateString = "{$set: {foo:'update'}}",
        };

        var result = await MongoDB.Update(input, MongoConnection, new Options(), default);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(1, result.Count);
        Assert.IsTrue(GetDocuments("update"));
    }

    [TestMethod]
    public async Task Test_Update_Single_UpdateMany()
    {
        var input = new Input()
        {
            InputType = InputType.Filter,
            UpdateOptions = Definitions.UpdateOptions.UpdateMany,
            Filter = "{'foo':'bar'}",
            Filters = null,
            File = null,
            UpdateString = "{$set: {foo:'update'}}",
        };

        var result = await MongoDB.Update(input, MongoConnection, new Options(), default);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(GetDocuments("update"));
    }

    [TestMethod]
    public async Task Test_Update_Array_UpdateOne()
    {
        var doc1 = new DocumentValues() { Value = "{'foo':'bar'}" };
        var doc2 = new DocumentValues() { Value = "{'qwe':'rty'}" };

        var input = new Input()
        {
            InputType = InputType.Filters,
            UpdateOptions = Definitions.UpdateOptions.UpdateOne,
            Filter = null,
            Filters = new[] { doc1, doc2 },
            File = null,
            UpdateString = "{$set: {foo:'update'}}",
        };

        var result = await MongoDB.Update(input, MongoConnection, new Options(), default);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(GetDocuments("update"));
    }

    [TestMethod]
    public async Task Test_Update_Array_UpdateMany()
    {
        var doc1 = new DocumentValues() { Value = "{'foo':'bar'}" };
        var doc2 = new DocumentValues() { Value = "{'qwe':'rty'}" };

        var input = new Input()
        {
            InputType = InputType.Filters,
            UpdateOptions = Definitions.UpdateOptions.UpdateMany,
            Filter = null,
            Filters = new[] { doc1, doc2 },
            File = null,
            UpdateString = "{$set: {foo:'update'}}",
        };

        var result = await MongoDB.Update(input, MongoConnection, new Options(), default);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(3, result.Count);
        Assert.IsTrue(GetDocuments("update"));
    }

    [TestMethod]
    public async Task Test_Update_File_UpdateOne()
    {
        var input = new Input()
        {
            InputType = InputType.File,
            UpdateOptions = Definitions.UpdateOptions.UpdateOne,
            Filter = null,
            Filters = null,
            File = "..//..//..//Files//testdata.json",
            UpdateString = "{$set: {foo:'update'}}",
        };

        var result = await MongoDB.Update(input, MongoConnection, new Options(), default);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(1, result.Count);
        Assert.IsTrue(GetDocuments("update"));
    }

    [TestMethod]
    public async Task Test_Update_File_UpdateMany()
    {
        var input = new Input()
        {
            InputType = InputType.File,
            UpdateOptions = Definitions.UpdateOptions.UpdateMany,
            Filter = null,
            Filters = null,
            File = "..//..//..//Files//testdata.json",
            UpdateString = "{$set: {foo:'update'}}",
        };

        var result = await MongoDB.Update(input, MongoConnection, new Options(), default);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(GetDocuments("update"));
    }

    [TestMethod]
    public async Task Test_InvalidConnectionString()
    {
        var input = new Input()
        {
            InputType = InputType.File,
            UpdateOptions = Definitions.UpdateOptions.UpdateMany,
            Filter = null,
            Filters = null,
            File = "..//..//..//Files//testdata.json",
            UpdateString = "{$set: {foo:'update'}}",
        };

        var connection = new Connection
        {
            ConnectionString = "mongodb://admin:Incorrect@localhost:27017/?authSource=invalid",
            CollectionName = MongoConnection.CollectionName,
            Database = MongoConnection.Database,
        };

        var ex = await Assert.ThrowsExceptionAsync<MongoAuthenticationException>(
            async () => await MongoDB.Update(input, connection, new Options(), default));
        Assert.IsTrue(ex.Message.StartsWith("Unable to authenticate using sasl protocol mechanism SCRAM-SHA-1."));
    }

    private void InsertTestData()
    {
        try
        {
            var collection = GetMongoCollection(
                MongoConnection.ConnectionString,
                MongoConnection.Database,
                MongoConnection.CollectionName);

            var doc1 = BsonDocument.Parse(document1Json);
            var doc2 = BsonDocument.Parse(document2Json);
            var doc3 = BsonDocument.Parse(document3Json);

            collection.InsertOne(doc1);
            collection.InsertOne(doc2);
            collection.InsertOne(doc3);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void UpdateTestData()
    {
        var collection = GetMongoCollection(
            MongoConnection.ConnectionString,
            MongoConnection.Database,
            MongoConnection.CollectionName);

        var filter1 = "{'bar':'foo'}";
        var filter2 = "{'qwe':'rty'}";
        var filter3 = "{'asd':'fgh'}";
        collection.DeleteMany(filter1);
        collection.DeleteMany(filter2);
        collection.DeleteMany(filter3);
    }

    private IMongoCollection<BsonDocument> GetMongoCollection(
        string connectionString,
        string database,
        string collectionName)
    {
        var dataBase = GetMongoDatabase(connectionString, database);
        var collection = dataBase.GetCollection<BsonDocument>(collectionName);
        return collection;
    }

    private IMongoDatabase GetMongoDatabase(string connectionString, string database)
    {
        var mongoClient = new MongoClient(connectionString);
        var dataBase = mongoClient.GetDatabase(database);
        return dataBase;
    }

    private bool GetDocuments(string updated)
    {
        var collection = GetMongoCollection(
            MongoConnection.ConnectionString,
            MongoConnection.Database,
            MongoConnection.CollectionName);
        var documents = collection.Find(new BsonDocument()).ToList();
        var i = documents.Any(x => x.Values.Contains(updated));
        return i;
    }
}
