using MongoDB.Bson.Serialization.Attributes;

namespace DMWGIS.API.Models
{
    [BsonIgnoreExtraElements]
    public class AssignVolunteer
    {
        public string userid { get; set; }
        public string alertid { get; set; }
        public string status { get; set; }
    }
}
