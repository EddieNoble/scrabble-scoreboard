namespace ScrabbleScoreboard.Result.Api.Persistence.Repositories.Interfaces
{
    public interface IResultRepository
    {
        Task<Domain.Result> AddAsync(Domain.Result result);

        Task<List<Domain.Result>> GetAllAsync();
    }
}
