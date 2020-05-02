using MongoDB.Bson.Serialization.Attributes;

namespace DMWGIS.API.Models
{
    [BsonIgnoreExtraElements]
    public class Type
    {
        public string type { get; set; }
    }
}
