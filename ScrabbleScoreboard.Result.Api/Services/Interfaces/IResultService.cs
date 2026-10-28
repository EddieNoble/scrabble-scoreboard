using ScrabbleScoreboard.Domain;

namespace ScrabbleScoreboard.Result.Api.Services.Interfaces
{
    /// <summary>
    /// Provides result data to the API.
    /// </summary>
    public interface IResultService
    {
        /// <summary>
        /// Gets all results.
        /// </summary>
        /// <returns>A collection of Results.</returns>
        Task<IEnumerable<Domain.Result>> GetResultsAsync();

        /// <summary>
        /// Gets a specified result.
        /// </summary>
        /// <param name="id">The unique ID of hte result.</param>
        /// <returns>A Result.</returns>
        Task<Domain.Result?> GetResultAsync(Guid id);

        /// <summary>
        /// Creates a new Result.
        /// </summary>
        /// <param name="result">The Result to add.</param>
        /// <returns>A Result.</returns>
        Task<Domain.Result> CreateResultAsync(Domain.Result result);

        /// <summary>
        /// Gets Leaderboard Entries for all players.
        /// </summary>
        /// <returns>A list of Leaderboard Entries.</returns>
        Task<IEnumerable<LeaderboardEntry>> GetLeaderboardAsync();
    }
}
