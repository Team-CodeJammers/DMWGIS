using DMWGIS.API.Adapter.Interface;
using DMWGIS.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DMWGIS.API.Controllers
{
    [Route("")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGetUserDataAdapter _getUserDataAdapter;
        public UserController(IGetUserDataAdapter getUserDataAdapter)
        {
            _getUserDataAdapter = getUserDataAdapter;
        }
        // GET: User
        [HttpGet("city",Name = "City")]
        public IActionResult GetCityList()
        {
            return Ok(_getUserDataAdapter.GetCities());
        }

        // GET: User
        [HttpGet("type", Name = "Type")]
        public IActionResult GetTypeList()
        {
            return Ok(_getUserDataAdapter.GetTypes());
        }

        // GET: User
        [HttpGet("Alerts/{userid}", Name = "Get")]
        public IActionResult GetNotificationList(string userid)
        {
            return Ok(_getUserDataAdapter.GetUserNotifications(userid));
        }

        // POST: User
        [HttpPost]
        public IActionResult Post(UserNotification addAlert)
        {
            try
            {
                _getUserDataAdapter.AddAlert(addAlert);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
