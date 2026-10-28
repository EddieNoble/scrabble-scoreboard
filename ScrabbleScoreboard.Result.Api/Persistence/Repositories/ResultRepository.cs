using Microsoft.EntityFrameworkCore;
using ScrabbleScoreboard.Result.Api.Persistence.Repositories.Interfaces;

namespace ScrabbleScoreboard.Result.Api.Persistence.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly ResultsDbContext _context;

        public ResultRepository(ResultsDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Result> AddAsync(Domain.Result result)
        {
            _context.Results.Add(result);

            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<List<Domain.Result>> GetAllAsync()
        {
            return await _context.Results.ToListAsync();
        }
    }
}
