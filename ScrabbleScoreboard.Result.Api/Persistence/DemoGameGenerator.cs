using ScrabbleScoreboard.Domain;
using ScrabbleScoreboard.Result.Api.DemoData;

namespace ScrabbleScoreboard.Result.Api.Persistence
{
    public class DemoGameGenerator
    {
        public List<Game> GenerateGames()
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

    }
}
