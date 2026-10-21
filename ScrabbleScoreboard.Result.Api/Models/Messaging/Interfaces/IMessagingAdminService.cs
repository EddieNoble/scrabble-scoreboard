using ScrabbleScoreboard.Result.Api.Models.Messaging.Contracts;

namespace ScrabbleScoreboard.Result.Api.Models.Messaging.Interfaces
{
    public interface IMessagingAdminService
    {
        Task PublishHealthMessageAsync();

        Task<List<PipelineHealthMessage>> GetHealthMessagesAsync();
    }
}
