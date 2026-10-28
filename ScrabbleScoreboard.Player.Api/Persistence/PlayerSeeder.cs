using ScrabbleScoreboard.Player.Api.DemoData;

namespace ScrabbleScoreboard.Player.Api.Persistence
{
    public static class PlayerSeeder
    {
        public static async Task SeedAsync(PlayersDbContext context)
        {
            if (context.Players.Any())
            {
                Console.WriteLine($"Existing players: {context.Players.Count()}");
                return;
            }

            context.Players.AddRange(DemoPlayers.Players);

            await context.SaveChangesAsync();

            Console.WriteLine($"Seeded players: {context.Players.Count()}");
        }
    }

}
