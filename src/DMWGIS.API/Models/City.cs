using MongoDB.Bson.Serialization.Attributes;

namespace DMWGIS.API.Models
{
    [BsonIgnoreExtraElements]
    public class City
    {
        public string city { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
}
