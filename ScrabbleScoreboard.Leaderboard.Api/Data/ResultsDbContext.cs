using Microsoft.EntityFrameworkCore;
using ScrabbleScoreboard.Domain;


namespace ScrabbleScoreboard.Leaderboard.Api.Data
{
    public class ResultsDbContext: DbContext
    {
        public ResultsDbContext(DbContextOptions<ResultsDbContext> options) : base(options)
        {            
        }

        public DbSet<Result> Results => Set<Result>();
    }
}
