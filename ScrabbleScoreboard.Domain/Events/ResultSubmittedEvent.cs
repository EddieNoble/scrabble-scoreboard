namespace ScrabbleScoreboard.Domain.Events;

public class ResultSubmittedEvent
{
    public Guid PlayerId { get; set; }
    public int Score { get; set; }
}