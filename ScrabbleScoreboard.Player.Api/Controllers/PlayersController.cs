using Microsoft.AspNetCore.Mvc;
using ScrabbleScoreboard.Player.Api.Services.Interfaces;

namespace ScrabbleScoreboard.Player.Api.Controllers
{
    /// <summary>
    /// Provides operations for managing and retrieving player information.
    /// </summary>
    [ApiController]
    [Route("api/players")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        /// <summary>
        /// Initialises a new instance of the <see cref="PlayersController"/> class.
        /// </summary>
        /// <param name="playerService">
        /// Service used to retrieve player information.
        /// </param>
        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        /// <summary>
        /// Retrieves all registered players.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _playerService.GetPlayersAsync());
        }

        /// <summary>
        /// Retrieves a player by identifier.
        /// </summary>
        /// <param name="id">The unique player identifier.</param>
        /// <returns>The matching player if found.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var player = await _playerService.GetPlayerAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        /// <summary>
        /// Adds a new player.
        /// </summary>
        /// <param name="player">The player to add.</param>
        /// <returns>The created player.</returns>
        [HttpPost]
        public IActionResult Create([FromBody] Domain.Player player)
        {
            return Ok(player);
        }
    }
}
    