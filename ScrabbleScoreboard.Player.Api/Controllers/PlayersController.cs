using Microsoft.AspNetCore.Mvc;
using ScrabbleScoreboard.Domain.Events;
using ScrabbleScoreboard.Player.Api.Infrastructure.Consumers;
using ScrabbleScoreboard.Player.Api.Infrastructure.Publishers;
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
        public async Task<IActionResult> Create([FromBody] Domain.Player player)
        {
            return Ok(await _playerService.AddPlayerAsync(player));
            
        }
        
        /// <summary>
        /// Adds a new player.
        /// </summary>
        /// <param name="request">A Create Player Request.</param>
        /// <returns>The created player.</returns>
        [HttpPost("create-from-dto")]
        public async Task<IActionResult> Create([FromBody] Domain.CreatePlayerRequest request)
        {
            var player = new Domain.Player{ Name = request.Name, EmailAddress = request.EmailAddress };
            return Ok(await _playerService.AddPlayerAsync(player));
            
        }

        /// <summary>
        /// Test consume of publisher
        /// </summary>
        /// <returns>Completed task.</returns>
        [HttpGet("consume")]
        public async Task<IActionResult> Consume()
        {
            var consumer = new PlayerCreatedConsumer();
            await consumer.ConsumeAsync();
            return Ok();
        }

        [HttpGet("consume-all")]
        public async Task<IActionResult> ConsumeAll()
        {
            var players = new List<PlayerCreatedEvent>();
            var consumer = new PlayerCreatedConsumer();
            while (true)
            {
                var player =
                    await consumer.GetNextMessageAsync();

                if (player == null)
                {
                    break;
                }

                players.Add(player);
            }

            return Ok(players);
        }


        /// <summary>
        /// Returns length of the queue.
        /// </summary>
        /// <returns>An int</returns>
        [HttpGet("queue-length")]
        public async Task<IActionResult> QueueLength()
        {
            var consumer = new PlayerCreatedConsumer();

            var count = await consumer.CurrentQueueLengthAsync();

            return Ok(count);
        }


    }
}
    