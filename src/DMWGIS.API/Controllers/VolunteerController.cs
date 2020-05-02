using DMWGIS.API.Adapter.Interface;
using DMWGIS.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DMWGIS.API.Controllers
{
    [Route("")]
    [ApiController]
    public class VolunteerController : ControllerBase
    {
        private readonly IVolunteerDetailsAdapter _volunteerDetailsAdapter;
        public VolunteerController(IVolunteerDetailsAdapter volunteerDetailsAdapter)
        {
            _volunteerDetailsAdapter = volunteerDetailsAdapter;
        }
        // GET: Volunteer
        [HttpGet("AssignedAlert", Name = "GetVolunteerAssignmentList")]
        public IActionResult GetVolunteerAssignmentList(string userid)
        {
            return Ok(_volunteerDetailsAdapter.GetAssignmentList(userid));
        }

        // POST: Volunteer
        [HttpPost("AddVolunteer", Name = "BecomeVolunteer")]
        public IActionResult BecomeVolunteer(Volunteer volunteer)
        {
            try
            {
                _volunteerDetailsAdapter.BecomeVolunteer(volunteer);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }

}
