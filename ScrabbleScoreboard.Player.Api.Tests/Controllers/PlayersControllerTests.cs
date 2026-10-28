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

    [TestMethod]
    public async Task Get_Returns_Ok_With_Empty_Players()
    {
        // Arrange

        var players = new List<Domain.Player>();

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
        Assert.AreSame(players, okResult.Value);
        playerService.Verify(x => x.GetPlayersAsync(), Times.Once);
    }

    [TestMethod]
    public async Task Get_By_Id_Returns_Ok_With_Player()
    {
        // Arrange

        var player = new Domain.Player
        {
            Id = Guid.NewGuid(),
            Name = "Karl Kruger",
            EmailAddress = "karl@example.com"
        };

        var playerService = new Mock<IPlayerService>();

        playerService
            .Setup(x => x.GetPlayerAsync(player.Id))
            .ReturnsAsync(player);

        var controller =
            new PlayersController(playerService.Object);

        // Act

        var result = await controller.Get(player.Id);

        // Assert

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));

        var okResult = (OkObjectResult)result;

        Assert.AreSame(player, okResult.Value);
        playerService.Verify(x => x.GetPlayerAsync(player.Id), Times.Once);
    }

    [TestMethod]
    public async Task Get_By_Id_Returns_NotFound_When_Player_Does_Not_Exist()
    {
        // Arrange

        var id = Guid.NewGuid();
        var playerService = new Mock<IPlayerService>();

        playerService
            .Setup(x => x.GetPlayerAsync(id))
            .ReturnsAsync((Domain.Player?)null);

        var controller =
            new PlayersController(playerService.Object);

        // Act

        var result = await controller.Get(id);

        // Assert

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        playerService.Verify(x => x.GetPlayerAsync(id), Times.Once);
    }

    [TestMethod]
    public async Task Get_By_Empty_Id_Returns_NotFound()
    {
        // Arrange

        var id = Guid.Empty;
        var playerService = new Mock<IPlayerService>();

        playerService
            .Setup(x => x.GetPlayerAsync(id))
            .ReturnsAsync((Domain.Player?)null);

        var controller =
            new PlayersController(playerService.Object);

        // Act

        var result = await controller.Get(id);

        // Assert

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        playerService.Verify(x => x.GetPlayerAsync(id), Times.Once);
    }

    [TestMethod]
    public async Task Create_Returns_Ok_With_Created_Player()
    {
        // Arrange

        var player = new Domain.Player
        {
            Id = Guid.NewGuid(),
            Name = "Karl Kruger",
            EmailAddress = "karl@example.com"
        };

        var createdPlayer = new Domain.Player
        {
            Id = Guid.NewGuid(),
            Name = player.Name,
            EmailAddress = player.EmailAddress
        };

        var playerService = new Mock<IPlayerService>();

        playerService
            .Setup(x => x.AddPlayerAsync(player))
            .ReturnsAsync(createdPlayer);

        var controller =
            new PlayersController(playerService.Object);

        // Act

        var result = await controller.Create(player);

        // Assert

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));

        var okResult = (OkObjectResult)result;

        Assert.AreSame(createdPlayer, okResult.Value);
        playerService.Verify(x => x.AddPlayerAsync(player), Times.Once);
    }

    [TestMethod]
    public async Task Create_Passes_Player_With_Empty_Fields_To_Service()
    {
        // Arrange

        var player = new Domain.Player();
        var playerService = new Mock<IPlayerService>();

        playerService
            .Setup(x => x.AddPlayerAsync(player))
            .ReturnsAsync(player);

        var controller =
            new PlayersController(playerService.Object);

        // Act

        var result = await controller.Create(player);

        // Assert

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));

        var okResult = (OkObjectResult)result;

        Assert.AreSame(player, okResult.Value);
        Assert.AreEqual(string.Empty, player.Name);
        Assert.AreEqual(string.Empty, player.EmailAddress);
        playerService.Verify(x => x.AddPlayerAsync(player), Times.Once);
    }

    [TestMethod]
    public async Task Get_Throws_When_Player_Service_Fails()
    {
        // Arrange

        var exception = new InvalidOperationException("Player service failed.");
        var playerService = new Mock<IPlayerService>();

        playerService
            .Setup(x => x.GetPlayersAsync())
            .ThrowsAsync(exception);

        var controller =
            new PlayersController(playerService.Object);

        // Act & Assert

        try
        {
            await controller.Get();
            Assert.Fail("Expected the player service exception to propagate.");
        }
        catch (InvalidOperationException thrownException)
        {
            Assert.AreSame(exception, thrownException);
        }

        playerService.Verify(x => x.GetPlayersAsync(), Times.Once);
    }

    [TestMethod]
    public async Task Get_By_Id_Throws_When_Player_Service_Fails()
    {
        // Arrange

        var id = Guid.NewGuid();
        var exception = new InvalidOperationException("Player service failed.");
        var playerService = new Mock<IPlayerService>();

        playerService
            .Setup(x => x.GetPlayerAsync(id))
            .ThrowsAsync(exception);

        var controller =
            new PlayersController(playerService.Object);

        // Act & Assert

        try
        {
            await controller.Get(id);
            Assert.Fail("Expected the player service exception to propagate.");
        }
        catch (InvalidOperationException thrownException)
        {
            Assert.AreSame(exception, thrownException);
        }

        playerService.Verify(x => x.GetPlayerAsync(id), Times.Once);
    }

    [TestMethod]
    public async Task Create_Throws_When_Player_Service_Fails()
    {
        // Arrange

        var player = new Domain.Player
        {
            Id = Guid.NewGuid(),
            Name = "Karl Kruger",
            EmailAddress = "karl@example.com"
        };

        var exception = new InvalidOperationException("Player service failed.");
        var playerService = new Mock<IPlayerService>();

        playerService
            .Setup(x => x.AddPlayerAsync(player))
            .ThrowsAsync(exception);

        var controller =
            new PlayersController(playerService.Object);

        // Act & Assert

        try
        {
            await controller.Create(player);
            Assert.Fail("Expected the player service exception to propagate.");
        }
        catch (InvalidOperationException thrownException)
        {
            Assert.AreSame(exception, thrownException);
        }

        playerService.Verify(x => x.AddPlayerAsync(player), Times.Once);
    }
}
