using Microsoft.EntityFrameworkCore;
using ScrabbleScoreboard.Leaderboard.Api.Services.Interfaces;
using ScrabbleScoreboard.Domain;
using ScrabbleScoreboard.Leaderboard.Api.Data;

namespace ScrabbleScoreboard.Leaderboard.Api.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        // TO BE REPLACED with calls to Player and Results APIs. For PoC.
        private readonly PlayerDbContext _playersDbContext;
        private readonly ResultsDbContext _resultsDbContext;

        public LeaderboardService(PlayerDbContext playersDbcontext, ResultsDbContext resultsDbContext)
        {
            _playersDbContext = playersDbcontext;
            _resultsDbContext = resultsDbContext;
        }

        public async Task<List<LeaderboardEntry>> GenerateLeaderboardAsync()
        {
            var results = await _resultsDbContext.Results.ToListAsync();
            var players = await _playersDbContext.Players.ToListAsync();

            var leaderboard =
                players
                    .Select(player =>
                    {
                        var playerResults =
                            results
                                .Where(x => x.PlayerId == player.Id)
                                .ToList();

                        return new LeaderboardEntry
                        {
                            PlayerId = player.Id,
                            PlayerName = player.Name,
                            GamesPlayed = playerResults.Count,
                            TotalScore = playerResults.Sum(x => x.Score)
                        };
                    })
                    .OrderByDescending(x => x.TotalScore)
                    .ToList();

            for (var i = 0; i < leaderboard.Count; i++)
            {
                leaderboard[i].Position = i + 1;
            }

            return leaderboard;


        }
    }
}
