using Microsoft.AspNetCore.Mvc;

namespace ScrabbleScoreboard.Result.Api.Controllers
{
    [ApiController]
    [Route("api/results")]
    public class ResultsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new List<Domain.Result>
        {
            new()
            {
                Id = Guid.NewGuid(),
                PlayerId = Guid.NewGuid(),
                Score = 850
            }
        });
        }
    }
}
