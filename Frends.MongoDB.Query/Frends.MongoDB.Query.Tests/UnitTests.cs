using Frends.MongoDB.Query.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Frends.MongoDB.Query.Tests;

/// <summary>
/// Unit tests for the MongoDB Query task.
/// </summary>
[TestClass]
public class UnitTests
{
    /*
        Run command 'docker-compose up -d' in \Frends.MongoDB.Query.Tests\Files\
    */

    private readonly Connection connection = new()
    {
        ConnectionString = "mongodb://admin:Salakala@localhost:27017/?authSource=admin",
        Database = "testdb",
        CollectionName = "testcoll",
    };

    private readonly string doc1 = "{ \"foo\":\"bar\", \"bar\": \"foo\" }";
    private readonly string doc2 = "{ \"foo\":\"bar\", \"bar\": \"foo\" }";
    private readonly string doc3 = "{ \"qwe\":\"rty\", \"asd\": \"fgh\" }";

    /// <summary>
    /// Inserts the test documents before each test.
    /// </summary>
    [TestInitialize]
    public void StartUp()
    {
        InsertTestData();
    }

    /// <summary>
    /// Removes the test documents after each test.
    /// </summary>
    [TestCleanup]
    public void CleanUp()
    {
        DeleteTestData();
    }

    /// <summary>
    /// Verifies that a filter can return two matching documents.
    /// </summary>
    [TestMethod]
    public void Test_Query_TwoResults()
    {
        var input = new Input
        {
            Filter = "{'foo':'bar'}",
        };

        var result = MongoDB.Query(input, connection, new Options(), default);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(2, result.Data.Count);
    }

    /// <summary>
    /// Verifies that a filter can return one matching document.
    /// </summary>
    [TestMethod]
    public void Test_Query_OneResults()
    {
        var input = new Input
        {
            Filter = "{'qwe':'rty'}",
        };

        var result = MongoDB.Query(input, connection, new Options(), default);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(1, result.Data.Count);
    }

    /// <summary>
    /// Verifies that a filter with no matches returns an empty result.
    /// </summary>
    [TestMethod]
    public void Test_Query_NotFoundFilter()
    {
        var input = new Input
        {
            Filter = "{'not':'found'}",
        };

        var result = MongoDB.Query(input, connection, new Options(), default);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(0, result.Data.Count);
    }

    /// <summary>
    /// Verifies that an invalid connection string reports an authentication error.
    /// </summary>
    [TestMethod]
    public void Test_InvalidConnectionString()
    {
        var input = new Input
        {
            Filter = "{'not':'found'}",
        };

        var invalidConnection = new Connection
        {
            ConnectionString = "mongodb://admin:Incorrect@localhost:27017/?authSource=invalid",
            CollectionName = connection.CollectionName,
            Database = connection.Database,
        };

        var ex = Assert.ThrowsException<Exception>(
            () => MongoDB.Query(input, invalidConnection, new Options(), default));
        Assert.IsTrue(
            ex.Message.StartsWith(
                "Query error: MongoDB.Driver.MongoAuthenticationException: Unable to authenticate using sasl protocol mechanism SCRAM-SHA-1."));
    }

    private void InsertTestData()
    {
        var collection = GetMongoCollection(
            connection.ConnectionString,
            connection.Database,
            connection.CollectionName);

        var firstDocument = BsonDocument.Parse(doc1);
        var secondDocument = BsonDocument.Parse(doc2);
        var thirdDocument = BsonDocument.Parse(doc3);

        collection.InsertOne(firstDocument);
        collection.InsertOne(secondDocument);
        collection.InsertOne(thirdDocument);
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

    private void DeleteTestData()
    {
        var collection = GetMongoCollection(
            connection.ConnectionString,
            connection.Database,
            connection.CollectionName);

        var filter1 = "{'bar':'foo'}";
        var filter2 = "{'qwe':'rty'}";
        var filter3 = "{'asd':'fgh'}";
        collection.DeleteMany(filter1);
        collection.DeleteMany(filter2);
        collection.DeleteMany(filter3);
    }
}
