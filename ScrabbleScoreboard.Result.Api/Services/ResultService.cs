using ScrabbleScoreboard.Domain;
using ScrabbleScoreboard.Result.Api.DemoData;
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

        public IEnumerable<LeaderboardEntry> GetLeaderboard()
        {
            return GenerateLeaderboard();
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


    }
}
