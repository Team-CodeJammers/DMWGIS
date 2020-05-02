using MongoDB.Bson.Serialization.Attributes;

namespace DMWGIS.API.Models
{
    [BsonIgnoreExtraElements]
    public class Volunteer
    {
        public string userid { get; set; }
        public string name { get; set; }
        public string occupation { get; set; }
        public string type { get; set; }
        public string city { get; set; }
        public bool active { get; set; }
    }
}
