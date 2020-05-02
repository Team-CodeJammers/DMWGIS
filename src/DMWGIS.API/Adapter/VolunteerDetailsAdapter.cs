using DMWGIS.API.Adapter.Interface;
using DMWGIS.API.Helper.Interface;
using DMWGIS.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;

namespace DMWGIS.API.Adapter
{
    internal class VolunteerDetailsAdapter:IVolunteerDetailsAdapter
    {
        private readonly IMongoClientHelper<Volunteer> _volunteerDetails;
        private readonly IMongoClientHelper<AssignVolunteer> _getAssignmentDetails;

        private readonly string _volunteerCollection;
        private readonly string _assignVolunteerCollection;

        public VolunteerDetailsAdapter(IMongoClientHelper<Volunteer> volunteerDetails,
            IMongoClientHelper<AssignVolunteer> getAssigmentDetails,IOptions<MongoDbSettings> settings)
        {
            _volunteerDetails = volunteerDetails;
            _getAssignmentDetails = getAssigmentDetails;
            _volunteerCollection = settings.Value.VolunteerCollection;
            _assignVolunteerCollection = settings.Value.VolunteerAssignCollection;
        }

        public List<AssignVolunteer> GetAssignmentList(string userid)
        {
            var filterUserId = Builders<AssignVolunteer>.Filter.Eq( f => f.userid, userid);
            var filterPending = Builders<AssignVolunteer>.Filter.Eq(f => f.status, "pending");
            var filterActive = Builders<AssignVolunteer>.Filter.Eq(f => f.status, "active");
            var orStatus = Builders<AssignVolunteer>.Filter.Or(filterPending,filterActive);
            var filterAssignment = Builders<AssignVolunteer>.Filter.And(filterUserId,orStatus);

            return _getAssignmentDetails.GetData(filterAssignment, _assignVolunteerCollection);
        }

        public void BecomeVolunteer(Volunteer userDetails)
        {
            _volunteerDetails.InsertOne(userDetails, _volunteerCollection);
        }

    }
}
