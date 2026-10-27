using Microsoft.EntityFrameworkCore;
using ScrabbleScoreboard.Domain;


namespace ScrabbleScoreboard.Leaderboard.Api.Data
{
    public class PlayerDbContext: DbContext
    {
        public PlayerDbContext(DbContextOptions<PlayerDbContext> options) : base(options)
        {            
        }
        public DbSet<Player> Players => Set<Player>();

    }
}
