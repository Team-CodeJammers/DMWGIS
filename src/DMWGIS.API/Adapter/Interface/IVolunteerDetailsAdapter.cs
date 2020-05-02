using DMWGIS.API.Models;
using System.Collections.Generic;

namespace DMWGIS.API.Adapter.Interface
{
    public interface IVolunteerDetailsAdapter
    {
        List<AssignVolunteer> GetAssignmentList(string userid);
        void BecomeVolunteer(Volunteer userDetails);
    }
}
