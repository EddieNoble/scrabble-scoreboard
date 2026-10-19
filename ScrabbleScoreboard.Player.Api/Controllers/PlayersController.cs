using Microsoft.AspNetCore.Mvc;
using ScrabbleScoreboard.Domain;

namespace ScrabbleScoreboard.Player.Api.Controllers
{
    [ApiController]
    [Route("api/players")]
    public class PlayersController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new List<Domain.Player>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Karl Kruger",
                    EmailAddress = "karl@example.com"
                }
            });
        }
    }
}
