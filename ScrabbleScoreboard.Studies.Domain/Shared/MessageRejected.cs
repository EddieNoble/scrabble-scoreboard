namespace ScrabbleScoreboard.Studies.Domain.Shared
{
    public sealed class MessageRejected
    {
        public Guid CorrelationId { get; set; }

        public string Reason { get; set; } = string.Empty;
    }
}
