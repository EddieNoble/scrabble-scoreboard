using Microsoft.EntityFrameworkCore;

namespace ScrabbleScoreboard.Player.Api.Persistence
{
    public class PlayersDbContext : DbContext
    {
        public DbSet<Domain.Player> Players => Set<Domain.Player>();
    }
}
