using Microsoft.EntityFrameworkCore;

namespace ScrabbleScoreboard.Result.Api.Persistence
{
    public class ResultsDbContext: DbContext
    {

        public DbSet<Domain.Result> Results => Set<Domain.Result>();

        public DbSet<Domain.LeaderboardEntry> LeaderboardEntries => Set<Domain.LeaderboardEntry>();

    }
}