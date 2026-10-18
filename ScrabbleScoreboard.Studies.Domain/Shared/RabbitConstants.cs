namespace ScrabbleScoreboard.Studies.Domain.Shared
{
    public static class RabbitConstants
    {
        public const string SubmittedExchange = "submitted";
        public const string ValidatedExchange = "validated";
        public const string ProcessedExchange = "processed";
        public const string RejectedExchange = "rejected";
        public const string ValidationQueue = "validation";
        public const string ProcessingQueue = "processing";
        public const string CompletionQueue = "completion";
        public const string CompensationQueue = "compensation";
    }
}
