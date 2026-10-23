using Microsoft.AspNetCore.Mvc;
using ScrabbleScoreboard.Leaderboard.Api.Services.Interfaces;

namespace ScrabbleScoreboard.Leaderboard.Api.Controllers
{
    /// <summary>
    /// Provides operations for retrieving performance information.
    /// </summary>
    [ApiController]
    [Route("api/leaderboard")]
    public class LeaderboardController : Controller
    {
        private readonly ILeaderboardService _leaderBoardService;

        public LeaderboardController(ILeaderboardService leaderBoardService)
        {
            _leaderBoardService = leaderBoardService;            
        }

        /// <summary>
        /// Returns performance data for players.
        /// </summary>
        /// <returns>A list of LeaderboardEntries</returns>
        [HttpGet()]
        public async Task<IActionResult> GetLeaderboard()
        {
            return Ok(await _leaderBoardService.GenerateLeaderboardAsync());
        }
    }
}
