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
        public async Task<IActionResult> Get()
        {
            return Ok(await _resultService.GetResultsAsync());
        }

        /// <summary>
        /// Retrieves a result by identifier.
        /// </summary>
        /// <param name="id">The unique result identifier.</param>
        /// <returns>The matching result if found.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _resultService.GetResultAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Adds a new result.
        /// </summary>
        /// <param name="result">The result to add.</param>
        /// <returns>The created result.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Domain.Result result)
        {
            return Ok(await _resultService.CreateResultAsync(result));
        }

        [HttpGet("leaderboard")]
        public async Task<IActionResult> GetLeaderboard()
        {
            return Ok(await _resultService.GetLeaderboardAsync());
        }
    }
}
