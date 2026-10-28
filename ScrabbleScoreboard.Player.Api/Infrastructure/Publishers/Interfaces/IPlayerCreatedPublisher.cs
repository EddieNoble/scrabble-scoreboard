using ScrabbleScoreboard.Domain.Events;

namespace ScrabbleScoreboard.Player.Api.Infrastructure.Publishers.Interfaces;

public interface IPlayerCreatedPublisher
{
    Task PublishAsync(PlayerCreatedEvent playerCreated);
}