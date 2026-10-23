using Microsoft.EntityFrameworkCore;

namespace ScrabbleScoreboard.Player.Api.Persistence
{
    public class PlayersDbContext : DbContext
    {
        public PlayersDbContext(DbContextOptions<PlayersDbContext> options): base(options)
        {
        }

        public DbSet<Domain.Player> Players => Set<Domain.Player>();
    }
}
