namespace ScrabbleScoreboard.Domain.Events;

public class PlayerCreatedEvent
{
    public Guid EventId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;

    public DateTime OccurredAtUtc { get; set; }
}