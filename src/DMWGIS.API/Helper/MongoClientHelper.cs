using DMWGIS.API.Helper.Interface;
using DMWGIS.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DMWGIS.API.Helper
{
    internal class MongoClientHelper<TRequest> : IMongoClientHelper<TRequest>
    {
        private readonly IMongoClient mongoClient;
        private readonly IMongoDatabase mongoDb;
        public MongoClientHelper(IOptions<MongoDbSettings> settings)
        {
            mongoClient = new MongoClient(settings.Value.ConnectionString);
            mongoDb= mongoClient.GetDatabase(settings.Value.DbName);
        }

        public void InsertOne(TRequest request, string collection)
            => mongoDb.GetCollection<TRequest>(collection).InsertOne(request);

        public List<TRequest> GetData(FilterDefinition<TRequest> filters, string collection)
            => mongoDb.GetCollection<TRequest>(collection).Find<TRequest>(filters).ToList();
    }
}
