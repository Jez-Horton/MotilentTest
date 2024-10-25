using Microsoft.AspNetCore.Mvc;
using EntrolyticsNotifier.Api.Models;


namespace EntrolyticsNotifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        [HttpPost("test")]
        public IActionResult TestNotificationContent([FromBody] NotificationContent content)
        {
            if (content == null)
                return BadRequest("Notification content is required.");

            return Ok(content);
        }
    }
}
