namespace ScrabbleScoreboard.Result.Api.Models.Messaging.Interfaces
{
    public interface IMessagePublisher
    {
        Task PublishHealthMessageAsync();
    }
}
