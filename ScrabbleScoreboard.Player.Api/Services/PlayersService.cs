using ScrabbleScoreboard.Player.Api.Persistence.Repositories.Interfaces;
using ScrabbleScoreboard.Player.Api.Services.Interfaces;

namespace ScrabbleScoreboard.Player.Api.Services
{
    public class PlayersService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepo;

        public PlayersService(IPlayerRepository playerRepo)
        {
            _playerRepo = playerRepo;
        }

        public async Task<IEnumerable<Domain.Player>> GetPlayersAsync()
        {
            return await _playerRepo.GetAllAsync();
        }

        public async Task<Domain.Player?> GetPlayerAsync(Guid id)
        {
            var allPlayers = await _playerRepo.GetAllAsync(); 

            return allPlayers.SingleOrDefault(p => p.Id == id);
        }

        public async Task<Domain.Player> AddPlayerAsync(Domain.Player player)
        {
            return await _playerRepo.AddAsync(player);
        }
    }
}
