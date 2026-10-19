using ScrabbleScoreboard.Player.Api.DemoData;
using ScrabbleScoreboard.Player.Api.Services.Interfaces;

namespace ScrabbleScoreboard.Player.Api.Services
{
    public class PlayersService : IPlayerService
    {
        public IEnumerable<Domain.Player> GetPlayers()
        {
            return DemoPlayers.Players;
        }

        public Domain.Player? GetPlayer(Guid id)
        {
            return DemoPlayers.Players.FirstOrDefault(x => x.Id == id);
        }
    }
}
