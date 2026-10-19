using ScrabbleScoreboard.Result.Api.Services.Interfaces;

namespace ScrabbleScoreboard.Result.Api.Services
{
    public class ResultService : IResultService
    {
        public Domain.Result? GetResult(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.Result> GetResults()
        {
            throw new NotImplementedException();
        }
    }
}
