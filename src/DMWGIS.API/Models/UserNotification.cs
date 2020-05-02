using MongoDB.Bson.Serialization.Attributes;

namespace DMWGIS.API.Models
{
    [BsonIgnoreExtraElements]
    public class UserNotification
    {
        public string alertid { get; set; }
        public string userid { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string city { get; set; }
        public string eventtype { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string createtime { get; set; }
    }
}
