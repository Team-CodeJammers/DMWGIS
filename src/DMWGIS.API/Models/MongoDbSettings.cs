namespace DMWGIS.API.Models
{
    internal class MongoDbSettings
    {
        internal string ConnectionString { get; set; }
        internal string DbName { get; set; }
        internal string AlertNotificationCollection { get; set; }
        internal string VolunteerAssignCollection { get; set; }
        internal string CityMasterCollection { get; set; }
        internal string VolunteerCollection { get; set; }
        internal string TypeCollection { get; set; }
    }
}
