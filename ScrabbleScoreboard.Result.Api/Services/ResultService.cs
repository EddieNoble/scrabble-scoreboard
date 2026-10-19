using ScrabbleScoreboard.Result.Api.Services.Interfaces;

namespace ScrabbleScoreboard.Result.Api.Services
{
    public class ResultService : IResultService
    {
        public Domain.Result? GetResult(Guid id)
        {
            return new Domain.Result
            {
                Id = id,
                PlayerId = Guid.NewGuid(),
                Score = 99
            };
        }

        public IEnumerable<Domain.Result> GetResults()
        {
            return new List<Domain.Result>
            {
                new Domain.Result
                {
                    Id = Guid.NewGuid(),
                    PlayerId = Guid.NewGuid(),
                    Score = 99
                },
                new Domain.Result
                {
                    Id = Guid.NewGuid(),
                    PlayerId = Guid.NewGuid(),
                    Score = 133
                }
            };
        }
    }
}
