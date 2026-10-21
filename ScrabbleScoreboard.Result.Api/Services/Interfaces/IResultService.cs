using ScrabbleScoreboard.Domain;

namespace ScrabbleScoreboard.Result.Api.Services.Interfaces
{
    /// <summary>
    /// Provides result data to the API.
    /// </summary>
    public interface IResultService
    {
        IEnumerable<Domain.Result> GetResults();

        Domain.Result? GetResult(Guid id);

        IEnumerable<LeaderboardEntry> GetLeaderboard();
    }
}
