using Microsoft.AspNetCore.Mvc;
using ScrabbleScoreboard.Result.Api.Services.Interfaces;

namespace ScrabbleScoreboard.Result.Api.Controllers
{
    /// <summary>
    /// Provides operations for managing and retrieving game results.
    /// </summary>
    [ApiController]
    [Route("api/results")]
    public class ResultsController : ControllerBase
    {
        private readonly IResultService _resultService;

        /// <summary>
        /// Initialises a new instance of the <see cref="ResultsController"/> class.
        /// </summary>
        /// <param name="resultService">
        /// Service used to retrieve game result information.
        /// </param>
        public ResultsController(IResultService resultService)
        {
            _resultService = resultService;
        }

        /// <summary>
        /// Retrieves all recorded game results.
        /// </summary>
        /// <returns>A collection of results.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_resultService.GetResults());
        }

        /// <summary>
        /// Retrieves a result by identifier.
        /// </summary>
        /// <param name="id">The unique result identifier.</param>
        /// <returns>The matching result if found.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(Guid id)
        {
            var result = _resultService.GetResult(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("leaderboard")]
        public IActionResult GetLeaderboard()
        {
            return Ok(_resultService.GetLeaderboard());
        }
    }
}
