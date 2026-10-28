using Microsoft.EntityFrameworkCore;

namespace ScrabbleScoreboard.Result.Api.Persistence
{
    public class ResultsDbContext: DbContext
    {
        public ResultsDbContext(DbContextOptions<ResultsDbContext> options): base(options)
        {
        }

        public DbSet<Domain.Result> Results => Set<Domain.Result>();
    
    }
}