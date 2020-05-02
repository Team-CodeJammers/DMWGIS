using MongoDB.Driver;
using System.Collections.Generic;

namespace DMWGIS.API.Helper.Interface
{
    internal interface IMongoClientHelper<TRequest>
    {
        void InsertOne(TRequest request, string collection);
        List<TRequest> GetData(FilterDefinition<TRequest> filters, string collection);
    }
}
