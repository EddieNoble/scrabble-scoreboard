using LiteDB;

namespace ScrabbleScoreboard.Player.Api.Persistence
{
    public class LiteDbContext
    {
        private readonly LiteDatabase _database;

        public LiteDbContext()
        {
            _database =
                new LiteDatabase("ScrabbleScoreboard.db");
        }

        public ILiteCollection<Domain.Player> Players =>
            _database.GetCollection<Domain.Player>("players");
    }
}
