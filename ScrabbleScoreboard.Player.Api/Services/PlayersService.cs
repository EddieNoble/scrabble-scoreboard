using ScrabbleScoreboard.Player.Api.Services.Interfaces;

namespace ScrabbleScoreboard.Player.Api.Services
{
    public class PlayersService : IPlayerService
    {
        public IEnumerable<Domain.Player> GetPlayers()
        {
            return
            [
                new Domain.Player
            {
                Id = Guid.NewGuid(),
                Name = "Karl Kruger",
                EmailAddress = "karl@example.com"
            },
            new Domain.Player
            {
                Id = Guid.NewGuid(),
                Name = "Sandra Smith",
                EmailAddress = "sandra@example.com"
            }
            ];
        }

        public Domain.Player? GetPlayer(Guid id)
        {
            return GetPlayers().FirstOrDefault(); //GetPlayers().FirstOrDefault(x => x.Id == id);
        }
    }
}
