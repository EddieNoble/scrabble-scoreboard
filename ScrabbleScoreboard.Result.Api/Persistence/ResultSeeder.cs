namespace ScrabbleScoreboard.Result.Api.Persistence
{
    public static class ResultSeeder
    {
        public static async Task SeedAsync(ResultsDbContext context)
        {
            if (context.Results.Any())
            {
                Console.WriteLine($"Existing results: {context.Results.Count()}");
                return;
            }

            var generator = new DemoGameGenerator();

            var games = generator.GenerateGames();

            context.Results.AddRange(
                games.SelectMany(x => x.Results));

            await context.SaveChangesAsync();

            Console.WriteLine($"Seeded results: {context.Results.Count()}");
        }
    }
}
