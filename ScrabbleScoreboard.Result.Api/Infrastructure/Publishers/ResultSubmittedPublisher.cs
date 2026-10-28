using ScrabbleScoreboard.Domain.Events;
using ScrabbleScoreboard.Result.Api.Infrastructure.Publishers.Interfaces;

namespace ScrabbleScoreboard.Result.Api.Infrastructure.Publishers;

public class ResultSubmittedPublisher: IResultSubmittedPublisher
{
    public Task PublishAsync(ResultSubmittedEvent resultSubmitted)
    {
        throw new NotImplementedException();
    }
}