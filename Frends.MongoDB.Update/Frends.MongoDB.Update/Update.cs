using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Frends.MongoDB.Update.Definitions;
using Frends.MongoDB.Update.Helpers;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Frends.MongoDB.Update;

/// <summary>
/// MongoDB Task.
/// </summary>
public static class MongoDB
{
    /// <summary>
    /// MongoDB update operation.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.MongoDB.Update)
    /// </summary>
    /// <param name="input">Input parameters.</param>
    /// <param name="connection">Connection parameters.</param>
    /// <param name="options">Additional parameters.</param>
    /// <param name="cancellationToken">A cancellation token provided by Frends Platform.</param>
    /// <returns>object { bool Success, long Count, object Error { string Message, Exception AdditionalInfo } }</returns>
    public static async Task<Result> Update(
        [PropertyTab] Input input,
        [PropertyTab] Connection connection,
        [PropertyTab] Options options,
        CancellationToken cancellationToken)
    {
        options ??= new Options();

        try
        {
            if (string.IsNullOrWhiteSpace(input.UpdateString))
                throw new Exception("Update string missing.");

            var collection = GetMongoCollection(
                connection.ConnectionString,
                connection.Database,
                connection.CollectionName);
            switch (input.InputType)
            {
                case InputType.File:
                    var fileContent = await File.ReadAllTextAsync(input.File, cancellationToken);
                    return new Result
                    {
                        Success = true,
                        Count = await UpdateOperation(
                            input,
                            BsonDocument.Parse(fileContent),
                            collection,
                            cancellationToken),
                    };

                case InputType.Filter:
                    return new Result
                    {
                        Success = true,
                        Count = await UpdateOperation(
                            input,
                            BsonDocument.Parse(input.Filter),
                            collection,
                            cancellationToken),
                    };

                case InputType.Filters:
                    long count = 0;
                    foreach (var filterDocument in input.Filters)
                    {
                        count += await UpdateOperation(
                            input,
                            BsonDocument.Parse(filterDocument.Value),
                            collection,
                            cancellationToken);
                    }

                    return new Result
                    {
                        Success = true,
                        Count = count,
                    };

                default:
                    return new Result
                    {
                        Success = false,
                        Count = 0,
                    };
            }
        }
        catch (Exception ex)
        {
            return ex.Handle(options);
        }
    }

    private static async Task<long> UpdateOperation(
        Input input,
        BsonDocument filter,
        IMongoCollection<BsonDocument> collection,
        CancellationToken cancellationToken)
    {
        UpdateDefinition<BsonDocument> update = input.UpdateString;

        switch (input.UpdateOptions)
        {
            case Definitions.UpdateOptions.UpdateOne:
                var updateOne = await collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
                return updateOne.ModifiedCount;
            case Definitions.UpdateOptions.UpdateMany:
                var updateMany = await collection.UpdateManyAsync(filter, update, cancellationToken: cancellationToken);
                return updateMany.ModifiedCount;
            default:
                return 0;
        }
    }

    private static IMongoCollection<BsonDocument> GetMongoCollection(string connectionString, string database, string collectionName)
    {
        var dataBase = GetMongoDatabase(connectionString, database);
        return dataBase.GetCollection<BsonDocument>(collectionName);
    }

    private static IMongoDatabase GetMongoDatabase(string connectionString, string database)
    {
        var mongoClient = new MongoClient(connectionString);
        return mongoClient.GetDatabase(database);
    }
}
