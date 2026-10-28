using Moq;
using ScrabbleScoreboard.Player.Api.Persistence.Repositories.Interfaces;
using ScrabbleScoreboard.Player.Api.Services;

namespace ScrabbleScoreboard.Player.Api.Tests.Services
{
    [TestClass]
    public class PlayerServiceTests
    {
        [TestMethod]
        public void GetPlayers_ReturnsOk()
        {
            // Arrange

            // Act

            // Assert
        }

        [TestMethod]
        public void GetPlayer_Returns_Player()
        {
            // Arrange

            // Act

            // Assert
        }

        [TestMethod]
        public async Task GetPlayers_Returns_Repository_Players()
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

            var playerRepository = new Mock<IPlayerRepository>();

            playerRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(players);

            var service = new PlayersService(playerRepository.Object);

            // Act

            var result = await service.GetPlayersAsync();

            // Assert

            Assert.AreSame(players, result);
            playerRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetPlayers_Returns_Empty_Collection_When_Repository_Is_Empty()
        {
            // Arrange

            var players = new List<Domain.Player>();
            var playerRepository = new Mock<IPlayerRepository>();

            playerRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(players);

            var service = new PlayersService(playerRepository.Object);

            // Act

            var result = await service.GetPlayersAsync();

            // Assert

            Assert.AreSame(players, result);
            Assert.AreEqual(0, result.Count());
            playerRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetPlayers_Throws_When_Repository_Fails()
        {
            // Arrange

            var exception = new InvalidOperationException("Player repository failed.");
            var playerRepository = new Mock<IPlayerRepository>();

            playerRepository
                .Setup(x => x.GetAllAsync())
                .ThrowsAsync(exception);

            var service = new PlayersService(playerRepository.Object);

            // Act & Assert

            try
            {
                await service.GetPlayersAsync();
                Assert.Fail("Expected the repository exception to propagate.");
            }
            catch (InvalidOperationException thrownException)
            {
                Assert.AreSame(exception, thrownException);
            }

            playerRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetPlayer_Returns_Player_With_Matching_Id()
        {
            // Arrange

            var player = new Domain.Player
            {
                Id = Guid.NewGuid(),
                Name = "Karl Kruger",
                EmailAddress = "karl@example.com"
            };

            var playerRepository = new Mock<IPlayerRepository>();

            playerRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Domain.Player> { player });

            var service = new PlayersService(playerRepository.Object);

            // Act

            var result = await service.GetPlayerAsync(player.Id);

            // Assert

            Assert.AreSame(player, result);
            playerRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetPlayer_Returns_Null_When_Id_Is_Not_Found()
        {
            // Arrange

            var playerRepository = new Mock<IPlayerRepository>();

            playerRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Domain.Player>());

            var service = new PlayersService(playerRepository.Object);

            // Act

            var result = await service.GetPlayerAsync(Guid.NewGuid());

            // Assert

            Assert.IsNull(result);
            playerRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetPlayer_Returns_Null_When_Id_Is_Empty()
        {
            // Arrange

            var playerRepository = new Mock<IPlayerRepository>();

            playerRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Domain.Player>());

            var service = new PlayersService(playerRepository.Object);

            // Act

            var result = await service.GetPlayerAsync(Guid.Empty);

            // Assert

            Assert.IsNull(result);
            playerRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetPlayer_Throws_When_Multiple_Players_Have_Matching_Id()
        {
            // Arrange

            var id = Guid.NewGuid();
            var playerRepository = new Mock<IPlayerRepository>();

            playerRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Domain.Player>
                {
                    new() { Id = id },
                    new() { Id = id }
                });

            var service = new PlayersService(playerRepository.Object);

            // Act & Assert

            try
            {
                await service.GetPlayerAsync(id);
                Assert.Fail("Expected duplicate matching players to throw.");
            }
            catch (InvalidOperationException)
            {
            }

            playerRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetPlayer_Throws_When_Repository_Fails()
        {
            // Arrange

            var id = Guid.NewGuid();
            var exception = new InvalidOperationException("Player repository failed.");
            var playerRepository = new Mock<IPlayerRepository>();

            playerRepository
                .Setup(x => x.GetAllAsync())
                .ThrowsAsync(exception);

            var service = new PlayersService(playerRepository.Object);

            // Act & Assert

            try
            {
                await service.GetPlayerAsync(id);
                Assert.Fail("Expected the repository exception to propagate.");
            }
            catch (InvalidOperationException thrownException)
            {
                Assert.AreSame(exception, thrownException);
            }

            playerRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [TestMethod]
        public async Task AddPlayer_Returns_Repository_Player()
        {
            // Arrange

            var player = new Domain.Player
            {
                Id = Guid.NewGuid(),
                Name = "Karl Kruger",
                EmailAddress = "karl@example.com"
            };

            var playerRepository = new Mock<IPlayerRepository>();

            playerRepository
                .Setup(x => x.AddAsync(player))
                .ReturnsAsync(player);

            var service = new PlayersService(playerRepository.Object);

            // Act

            var result = await service.AddPlayerAsync(player);

            // Assert

            Assert.AreSame(player, result);
            playerRepository.Verify(x => x.AddAsync(player), Times.Once);
        }

        [TestMethod]
        public async Task AddPlayer_Passes_Player_With_Empty_Fields_To_Repository()
        {
            // Arrange

            var player = new Domain.Player();
            var playerRepository = new Mock<IPlayerRepository>();

            playerRepository
                .Setup(x => x.AddAsync(player))
                .ReturnsAsync(player);

            var service = new PlayersService(playerRepository.Object);

            // Act

            var result = await service.AddPlayerAsync(player);

            // Assert

            Assert.AreSame(player, result);
            Assert.AreEqual(string.Empty, player.Name);
            Assert.AreEqual(string.Empty, player.EmailAddress);
            playerRepository.Verify(x => x.AddAsync(player), Times.Once);
        }

        [TestMethod]
        public async Task AddPlayer_Throws_When_Repository_Fails()
        {
            // Arrange

            var player = new Domain.Player
            {
                Id = Guid.NewGuid(),
                Name = "Karl Kruger",
                EmailAddress = "karl@example.com"
            };

            var exception = new InvalidOperationException("Player repository failed.");
            var playerRepository = new Mock<IPlayerRepository>();

            playerRepository
                .Setup(x => x.AddAsync(player))
                .ThrowsAsync(exception);

            var service = new PlayersService(playerRepository.Object);

            // Act & Assert

            try
            {
                await service.AddPlayerAsync(player);
                Assert.Fail("Expected the repository exception to propagate.");
            }
            catch (InvalidOperationException thrownException)
            {
                Assert.AreSame(exception, thrownException);
            }

            playerRepository.Verify(x => x.AddAsync(player), Times.Once);
        }

    }
}
