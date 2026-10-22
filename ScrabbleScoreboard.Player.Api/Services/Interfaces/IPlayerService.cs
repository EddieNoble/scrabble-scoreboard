using ScrabbleScoreboard.Domain;

namespace ScrabbleScoreboard.Player.Api.Services.Interfaces
{
    /// <summary>
    /// Provides player data to the API.
    /// </summary>
    public interface IPlayerService
    {
        Task<IEnumerable<Domain.Player>> GetPlayersAsync();

        Task <Domain.Player?> GetPlayerAsync(Guid id);

        Task <Domain.Player>AddPlayerAsync(Domain.Player player);
    }

}
