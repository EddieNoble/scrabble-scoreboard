using Microsoft.EntityFrameworkCore;
using ScrabbleScoreboard.Domain;
using ScrabbleScoreboard.Leaderboard.Api.Data;
using ScrabbleScoreboard.Leaderboard.Api.Services;

namespace ScrabbleScoreboard.Leaderboard.Api.Tests.Services;

[TestClass]
public class LeaderboardServiceTests
{
    [TestMethod]
    public async Task GenerateLeaderboard_Returns_Entries_Ordered_By_Total_Score()
    {
        // Arrange

        var highScoringPlayer = new Player { Id = Guid.NewGuid(), Name = "High Scorer" };
        var lowScoringPlayer = new Player { Id = Guid.NewGuid(), Name = "Low Scorer" };
        var playerWithoutResults = new Player { Id = Guid.NewGuid(), Name = "No Results" };
        var playerDbContext = CreatePlayerContext(highScoringPlayer, lowScoringPlayer, playerWithoutResults);
        var resultsDbContext = CreateResultsContext(
            new Result { PlayerId = highScoringPlayer.Id, Score = 50 },
            new Result { PlayerId = highScoringPlayer.Id, Score = 25 },
            new Result { PlayerId = lowScoringPlayer.Id, Score = 40 });
        var service = new LeaderboardService(playerDbContext, resultsDbContext);

        // Act

        var entries = await service.GenerateLeaderboardAsync();

        // Assert

        Assert.HasCount(3, entries);
        Assert.AreEqual(highScoringPlayer.Id, entries[0].PlayerId);
        Assert.AreEqual(highScoringPlayer.Name, entries[0].PlayerName);
        Assert.AreEqual(2, entries[0].GamesPlayed);
        Assert.AreEqual(75, entries[0].TotalScore);
        Assert.AreEqual(1, entries[0].Position);
        Assert.AreEqual(lowScoringPlayer.Id, entries[1].PlayerId);
        Assert.AreEqual(1, entries[1].GamesPlayed);
        Assert.AreEqual(40, entries[1].TotalScore);
        Assert.AreEqual(2, entries[1].Position);
        Assert.AreEqual(playerWithoutResults.Id, entries[2].PlayerId);
        Assert.AreEqual(0, entries[2].GamesPlayed);
        Assert.AreEqual(0, entries[2].TotalScore);
        Assert.AreEqual(3, entries[2].Position);
    }

    [TestMethod]
    public async Task GenerateLeaderboard_Returns_Empty_When_There_Are_No_Players()
    {
        // Arrange

        var playerDbContext = CreatePlayerContext();
        var resultsDbContext = CreateResultsContext(new Result { PlayerId = Guid.NewGuid(), Score = 100 });
        var service = new LeaderboardService(playerDbContext, resultsDbContext);

        // Act

        var entries = await service.GenerateLeaderboardAsync();

        // Assert

        Assert.IsEmpty(entries);
    }

    [TestMethod]
    public async Task GenerateLeaderboard_Returns_Zero_Scores_When_There_Are_No_Results()
    {
        // Arrange

        var firstPlayer = new Player { Id = Guid.NewGuid(), Name = "First" };
        var secondPlayer = new Player { Id = Guid.NewGuid(), Name = "Second" };
        var playerDbContext = CreatePlayerContext(firstPlayer, secondPlayer);
        var resultsDbContext = CreateResultsContext();
        var service = new LeaderboardService(playerDbContext, resultsDbContext);

        // Act

        var entries = await service.GenerateLeaderboardAsync();

        // Assert

        Assert.HasCount(2, entries);
        Assert.IsTrue(entries.All(entry => entry.GamesPlayed == 0));
        Assert.IsTrue(entries.All(entry => entry.TotalScore == 0));
        Assert.AreEqual(1, entries[0].Position);
        Assert.AreEqual(2, entries[1].Position);
    }

    [TestMethod]
    public async Task GenerateLeaderboard_Includes_Zero_And_Negative_Scores()
    {
        // Arrange

        var player = new Player { Id = Guid.NewGuid(), Name = "Boundary Scorer" };
        var playerDbContext = CreatePlayerContext(player);
        var resultsDbContext = CreateResultsContext(
            new Result { PlayerId = player.Id, Score = 0 },
            new Result { PlayerId = player.Id, Score = -5 });
        var service = new LeaderboardService(playerDbContext, resultsDbContext);

        // Act

        var entries = await service.GenerateLeaderboardAsync();

        // Assert

        Assert.HasCount(1, entries);
        Assert.AreEqual(2, entries[0].GamesPlayed);
        Assert.AreEqual(-5, entries[0].TotalScore);
        Assert.AreEqual(1, entries[0].Position);
    }

    private static PlayerDbContext CreatePlayerContext(params Player[] players)
    {
        var options = new DbContextOptionsBuilder<PlayerDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new PlayerDbContext(options);

        context.Players.AddRange(players);
        context.SaveChanges();

        return context;
    }

    private static ResultsDbContext CreateResultsContext(params Result[] results)
    {
        var options = new DbContextOptionsBuilder<ResultsDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ResultsDbContext(options);

        context.Results.AddRange(results);
        context.SaveChanges();

        return context;
    }
}