using System;

namespace ScrabbleScoreboard.Studies.Domain.RabbitMQ.Fanout.Messages
{

	public class ResultSubmitted
	{
            public Guid ResultId { get; set; }

            public string PlayerName { get; set; } = string.Empty;

            public int Score { get; set; }

            public DateTime SubmittedAt { get; set; }
    }
}