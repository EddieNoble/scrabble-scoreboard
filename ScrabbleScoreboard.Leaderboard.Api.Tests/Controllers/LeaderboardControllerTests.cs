using Microsoft.AspNetCore.Mvc;
using Moq;
using ScrabbleScoreboard.Domain;
using ScrabbleScoreboard.Leaderboard.Api.Controllers;
using ScrabbleScoreboard.Leaderboard.Api.Services.Interfaces;

namespace ScrabbleScoreboard.Leaderboard.Api.Tests.Controllers;

[TestClass]
public class LeaderboardControllerTests
{
    [TestMethod]
    public async Task GetLeaderboard_Returns_Ok_With_Leaderboard_Entries()
    {
        // Arrange

        var entries = new List<LeaderboardEntry>
        {
            new()
            {
                PlayerId = Guid.NewGuid(),
                PlayerName = "Karl Kruger",
                GamesPlayed = 3,
                TotalScore = 120,
                Position = 1
            }
        };

        var leaderboardService = new Mock<ILeaderboardService>();

        leaderboardService
            .Setup(x => x.GenerateLeaderboardAsync())
            .ReturnsAsync(entries);

        var controller = new LeaderboardController(leaderboardService.Object);

        // Act

        var result = await controller.GetLeaderboard();

        // Assert

        var okResult = Assert.IsInstanceOfType<OkObjectResult>(result);

        Assert.AreSame(entries, okResult.Value);
        leaderboardService.Verify(x => x.GenerateLeaderboardAsync(), Times.Once);
    }

    [TestMethod]
    public async Task GetLeaderboard_Returns_Ok_With_Empty_Leaderboard()
    {
        // Arrange

        var entries = new List<LeaderboardEntry>();
        var leaderboardService = new Mock<ILeaderboardService>();

        leaderboardService
            .Setup(x => x.GenerateLeaderboardAsync())
            .ReturnsAsync(entries);

        var controller = new LeaderboardController(leaderboardService.Object);

        // Act

        var result = await controller.GetLeaderboard();

        // Assert

        var okResult = Assert.IsInstanceOfType<OkObjectResult>(result);

        Assert.AreSame(entries, okResult.Value);
        leaderboardService.Verify(x => x.GenerateLeaderboardAsync(), Times.Once);
    }

    [TestMethod]
    public async Task GetLeaderboard_Throws_When_Service_Fails()
    {
        // Arrange

        var exception = new InvalidOperationException("Leaderboard service failed.");
        var leaderboardService = new Mock<ILeaderboardService>();

        leaderboardService
            .Setup(x => x.GenerateLeaderboardAsync())
            .ThrowsAsync(exception);

        var controller = new LeaderboardController(leaderboardService.Object);

        // Act & Assert

        try
        {
            await controller.GetLeaderboard();
            Assert.Fail("Expected the service exception to propagate.");
        }
        catch (InvalidOperationException thrownException)
        {
            Assert.AreSame(exception, thrownException);
        }

        leaderboardService.Verify(x => x.GenerateLeaderboardAsync(), Times.Once);
    }
}