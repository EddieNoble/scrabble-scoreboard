namespace ScrabbleScoreboard.Player.Api.Persistence.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        Task<Domain.Player> AddAsync(Domain.Player player);

        Task<List<Domain.Player>> GetAllAsync();
    }
}
