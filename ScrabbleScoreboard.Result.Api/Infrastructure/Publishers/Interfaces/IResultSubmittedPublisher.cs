using ScrabbleScoreboard.Domain.Events;

namespace ScrabbleScoreboard.Result.Api.Infrastructure.Publishers.Interfaces;

public interface IResultSubmittedPublisher
{
    Task PublishAsync(ResultSubmittedEvent resultSubmitted);
}