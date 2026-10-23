using ScrabbleScoreboard.Domain;
using ScrabbleScoreboard.Result.Api.DemoData;
using ScrabbleScoreboard.Result.Api.Persistence.Repositories.Interfaces;
using ScrabbleScoreboard.Result.Api.Services.Interfaces;

namespace ScrabbleScoreboard.Result.Api.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;

        public ResultService(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;            
        }

        public async Task<Domain.Result?> GetResultAsync(Guid id)
        {
            var allResults = await _resultRepository.GetAllAsync();

            return allResults.SingleOrDefault(p => p.Id == id);
        }

        public async Task<IEnumerable<Domain.Result>> GetResultsAsync()
        {
            return await _resultRepository.GetAllAsync();
        }

        public async Task<IEnumerable<LeaderboardEntry>> GetLeaderboardAsync()
        {
            return await Task.FromResult(GenerateLeaderboard());
        }

        private List<Game> GenerateGames()
        {
            var players = DemoPlayers.Players;

            var games = new List<Game>();

            var scoreSeed = 250;

            for (var i = 0; i < players.Count; i++)
            {
                for (var j = i + 1; j < players.Count; j++)
                {
                    games.Add(
                        new Game
                        {
                            Id = Guid.NewGuid(),

                            Results =
                            [
                                new Domain.Result
                        {
                            Id = Guid.NewGuid(),
                            PlayerId = players[i].Id,
                            Score = scoreSeed + (i * 15)
                        },

                        new Domain.Result
                        {
                            Id = Guid.NewGuid(),
                            PlayerId = players[j].Id,
                            Score = scoreSeed + (j * 10)
                        }
                            ]
                        });

                    scoreSeed += 20;
                }
            }

            return games;
        }

        private List<LeaderboardEntry> GenerateLeaderboard()
        {
            var players = DemoPlayers.Players;

            var games = GenerateGames();

            var leaderboard =
                players
                    .Select(player =>
                    {
                        var playerResults =
                            games
                                .SelectMany(x => x.Results)
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

        public async Task<Domain.Result> CreateResultAsync(Domain.Result result)
        {
            return await _resultRepository.AddAsync(result);
        }
    }
}
