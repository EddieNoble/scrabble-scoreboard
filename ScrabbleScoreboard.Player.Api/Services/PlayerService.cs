using ScrabbleScoreboard.Player.Api.Services.Interfaces;

namespace ScrabbleScoreboard.Player.Api.Services
{
    public class PlayerService : IPlayerService
    {
        public Domain.Player? GetPlayer(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.Player> GetPlayers()
        {
            throw new NotImplementedException();
        }
    }
}
