namespace ScrabbleScoreboard.Result.Api.DemoData
{
    public static class DemoPlayers
    {
        public static readonly List<Domain.Player> Players =
            [
             new Domain.Player
                {
                    Id = new Guid("a9e1b91c-3f54-4140-9ace-d83bd8900e2d"),
                    Name = "Karl Kruger",
                    EmailAddress = "karl@example.com"
                },
            new Domain.Player
                {
                    Id = new Guid("1aa392bb-5ab6-4317-b59a-0ac50ad45f9c"),
                    Name = "Sandra Smith",
                    EmailAddress = "sandra@example.com"
                },
            new Domain.Player
                {
                    Id = new Guid("be34fd84-c8df-41fd-ab16-e0d72190cd8b"),
                    Name = "Alan Brown",
                    EmailAddress = "alan@example.com"
                },
            new Domain.Player
                {
                    Id = new Guid("57b7ce64-6fb0-4115-8d07-2874f259608b"),
                    Name = "Jane Wilson",
                    EmailAddress = "jane@example.com"
                },
            new Domain.Player
            {
                Id = new Guid("82e3fe8f-d432-4de5-9909-5dffdc4b5b77"),
                Name = "Mark Evans",
                EmailAddress = "mark@example.com"
            },
            new Domain.Player
            {
                Id = new Guid("6f27967a-c957-4a7a-8d96-7afdc4e34fb2"),
                Name = "Emily Jones",
                EmailAddress = "emily@example.com"
            }
        ];
    }
}
