using Microsoft.EntityFrameworkCore;
using ScrabbleScoreboard.Player.Api.Persistence.Repositories.Interfaces;

namespace ScrabbleScoreboard.Player.Api.Persistence.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly PlayersDbContext _context;

        public PlayerRepository(PlayersDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Player> AddAsync(Domain.Player player)
        {
            _context.Players.Add(player);

            await _context.SaveChangesAsync();

            return player;
        }

        public async Task<List<Domain.Player>> GetAllAsync()
        {
            return await _context.Players.ToListAsync();
        }
    }
}
