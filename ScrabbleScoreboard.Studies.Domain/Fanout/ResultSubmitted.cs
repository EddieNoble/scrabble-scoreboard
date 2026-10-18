namespace ScrabbleScoreboard.Studies.Domain.Fanout
{

	public class ResultSubmitted
	{
            public Guid ResultId { get; set; }

            public string PlayerName { get; set; } = string.Empty;

            public int Score { get; set; }

            public DateTime SubmittedAt { get; set; }
    }
}