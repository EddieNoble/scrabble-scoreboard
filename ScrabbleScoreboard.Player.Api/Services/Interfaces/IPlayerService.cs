using ScrabbleScoreboard.Domain;

namespace ScrabbleScoreboard.Player.Api.Services.Interfaces
{
    /// <summary>
    /// Provides player data to the API.
    /// </summary>
    public interface IPlayerService
    {
        IEnumerable<Domain.Player> GetPlayers();

        Domain.Player? GetPlayer(Guid id);
    }

}
