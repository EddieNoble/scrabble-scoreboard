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
        private readonly IMessagePublisher _publisher;

        public MessagingController(IMessagePublisher publisher)
        {
            _publisher = publisher;
        }

        /// <summary>
        /// Indicates the messaging system is healthy.
        /// </summary>
        /// <returns>OK.</returns>
        [HttpPost("health")]
        public async Task<IActionResult> Health()
        {
            await _publisher.PublishHealthMessageAsync();

            return Ok(
                "Pipeline health message published.");
        }
    }
}
