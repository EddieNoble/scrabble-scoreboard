using Microsoft.AspNetCore.Mvc;
using Moq;
using ScrabbleScoreboard.Player.Api.Controllers;
using ScrabbleScoreboard.Player.Api.Services.Interfaces;

namespace ScrabbleScoreboard.Player.Api.Tests.Controllers;

[TestClass]
public class PlayersControllerTests
{
    [TestMethod]
    public async Task Get_Returns_Ok_With_Players()
    {
        // Arrange

        var players = new List<Domain.Player>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Karl Kruger",
                EmailAddress = "karl@example.com"
            }
        };

        var playerService = new Mock<IPlayerService>();

        playerService
            .Setup(x => x.GetPlayersAsync())
            .ReturnsAsync(players);

        var controller =
            new PlayersController(playerService.Object);

        // Act

        var result = await controller.Get();

        // Assert

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));

        var okResult = (OkObjectResult)result;

        Assert.IsNotNull(okResult.Value);

        var returnedPlayers =
            (IEnumerable<Domain.Player>)okResult.Value;

        Assert.AreEqual(1, returnedPlayers.Count());
    }
}
