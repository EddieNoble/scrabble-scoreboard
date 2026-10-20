using Microsoft.AspNetCore.Mvc;
using ScrabbleScoreboard.Result.Api.Models.Messaging.Interfaces;

namespace ScrabbleScoreboard.Result.Api.Controllers
{
    /// <summary>
    /// Provides endpoints for monitoring messagin system health and providing serive information.
    /// </summary>
    [ApiController]
    [Route("api/messaging")]
    public class MessagingController: ControllerBase
    {
        private readonly IMessagingAdminService _adminService;

        public MessagingController(IMessagingAdminService adminService)
        {
            _adminService = adminService;
        }

        /// <summary>
        /// Indicates the messaging system is healthy.
        /// </summary>
        /// <returns>OK.</returns>
        [HttpPost("health")]
        public async Task<IActionResult> Health()
        {
            await _adminService.PublishHealthMessageAsync();

            return Ok(new
            {
                Message = "Pipeline health message published."
            });
        }

        [HttpGet("health/messages")]
        public async Task<IActionResult> GetHealthMessages()
        {
            var messages =
                await _adminService.GetHealthMessagesAsync();

            return Ok(messages);
        }
    }
}
