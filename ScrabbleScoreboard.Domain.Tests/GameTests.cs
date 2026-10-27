namespace ScrabbleScoreboard.Domain.Tests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void Game_With_One_Player_Is_Invalid()
        {
            // Arrange

            var game = new Game();
            var player =  new Player { Id = Guid.NewGuid()  };  
            game.Results.Add(new Result { PlayerId = player.Id });

            // Act

            var result = game.IsValid();

            // Assert

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Game_With_Two_Players_Is_Valid()
        {
            // Arrange

            var game = new Game();
            var players = new List<Player> { 
                    new Player { Id = Guid.NewGuid() },
                    new Player { Id = Guid.NewGuid() }
                };

            foreach (var player in players) 
            {
                game.Results.Add(new Result { PlayerId = player.Id });
            }

            // Act

            var result = game.IsValid();

            // Assert

            Assert.IsTrue(result);

        }


        [TestMethod]
        public void Game_With_Four_Players_Is_Valid()
        {
            // Arrange

            var game = new Game();
            var players = new List<Player> {
                new Player { Id = Guid.NewGuid() },
                new Player { Id = Guid.NewGuid() },
                new Player { Id = Guid.NewGuid() },
                new Player { Id = Guid.NewGuid() }
                };

            foreach (var player in players)
            {
                game.Results.Add(new Result { PlayerId = player.Id });
            }

            // Act

            var result = game.IsValid();

            // Assert

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void Game_With_Five_Players_Is_Invalid()
        {
            // Arrange

            var game = new Game();
            var players = new List<Player> {
                new Player { Id = Guid.NewGuid() },
                new Player { Id = Guid.NewGuid() },
                new Player { Id = Guid.NewGuid() },
                new Player { Id = Guid.NewGuid() },
                new Player { Id = Guid.NewGuid() }
                };

            foreach (var player in players)
            {
                game.Results.Add(new Result { PlayerId = player.Id });
            }

            // Act

            var result = game.IsValid();

            // Assert

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Game_With_No_Players_Is_Invalid()
        {
            // Arrange

            var game = new Game();

            // Act

            var result = game.IsValid();

            // Assert

            Assert.IsFalse(result);
        }

    }
}
