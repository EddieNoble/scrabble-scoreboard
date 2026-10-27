using ScrabbleScoreboard.Domain;

namespace ScrabbleScoreboard.Leaderboard.Api.Services.Interfaces
{
    public interface ILeaderboardService
    {
        Task<List<LeaderboardEntry>> GenerateLeaderboardAsync();
    }
}
