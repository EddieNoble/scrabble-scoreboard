namespace ScrabbleScoreboard.Result.Api.Tests.Services
{
    [TestClass]
    public class ResultServiceTests
    {
        [TestMethod]
        public async Task GetResults_Returns_Ok()
        {
            // Arrange

            // Act

            // Assert
        }

        [TestMethod]
        public async Task GetResult_Returns_Result()
        {
            // Arrange

            // Act

            // Assert
        }

        [TestMethod]
        public async Task GetResults_Returns_Repository_Results()
        {
            // Arrange

            var results = new List<ScrabbleScoreboard.Domain.Result>
            {
                new() { Id = Guid.NewGuid(), PlayerId = Guid.NewGuid(), Score = 42 }
            };

            var repository = new FakeResultRepository { Results = results };
            var service = new ScrabbleScoreboard.Result.Api.Services.ResultService(repository);

            // Act

            var returnedResults = await service.GetResultsAsync();

            // Assert

            Assert.AreSame(results, returnedResults);
            Assert.AreEqual(1, repository.GetAllCallCount);
        }

        [TestMethod]
        public async Task GetResults_Returns_Empty_Collection_When_Repository_Is_Empty()
        {
            // Arrange

            var results = new List<ScrabbleScoreboard.Domain.Result>();
            var repository = new FakeResultRepository { Results = results };
            var service = new ScrabbleScoreboard.Result.Api.Services.ResultService(repository);

            // Act

            var returnedResults = await service.GetResultsAsync();

            // Assert

            Assert.AreSame(results, returnedResults);
            Assert.AreEqual(0, returnedResults.Count());
            Assert.AreEqual(1, repository.GetAllCallCount);
        }

        [TestMethod]
        public async Task GetResults_Throws_When_Repository_Fails()
        {
            // Arrange

            var exception = new InvalidOperationException("Result repository failed.");
            var repository = new FakeResultRepository { GetAllException = exception };
            var service = new ScrabbleScoreboard.Result.Api.Services.ResultService(repository);

            // Act & Assert

            try
            {
                await service.GetResultsAsync();
                Assert.Fail("Expected the repository exception to propagate.");
            }
            catch (InvalidOperationException thrownException)
            {
                Assert.AreSame(exception, thrownException);
            }

            Assert.AreEqual(1, repository.GetAllCallCount);
        }

        [TestMethod]
        public async Task GetResult_Returns_Result_With_Matching_Id()
        {
            // Arrange

            var result = new ScrabbleScoreboard.Domain.Result
            {
                Id = Guid.NewGuid(),
                PlayerId = Guid.NewGuid(),
                Score = 42
            };

            var repository = new FakeResultRepository
            {
                Results = new List<ScrabbleScoreboard.Domain.Result> { result }
            };
            var service = new ScrabbleScoreboard.Result.Api.Services.ResultService(repository);

            // Act

            var returnedResult = await service.GetResultAsync(result.Id);

            // Assert

            Assert.AreSame(result, returnedResult);
            Assert.AreEqual(1, repository.GetAllCallCount);
        }

        [TestMethod]
        public async Task GetResult_Returns_Null_When_Id_Is_Not_Found()
        {
            // Arrange

            var repository = new FakeResultRepository();
            var service = new ScrabbleScoreboard.Result.Api.Services.ResultService(repository);

            // Act

            var returnedResult = await service.GetResultAsync(Guid.NewGuid());

            // Assert

            Assert.IsNull(returnedResult);
            Assert.AreEqual(1, repository.GetAllCallCount);
        }

        [TestMethod]
        public async Task GetResult_Returns_Null_When_Id_Is_Empty()
        {
            // Arrange

            var repository = new FakeResultRepository();
            var service = new ScrabbleScoreboard.Result.Api.Services.ResultService(repository);

            // Act

            var returnedResult = await service.GetResultAsync(Guid.Empty);

            // Assert

            Assert.IsNull(returnedResult);
            Assert.AreEqual(1, repository.GetAllCallCount);
        }

        [TestMethod]
        public async Task GetResult_Throws_When_Multiple_Results_Have_Matching_Id()
        {
            // Arrange

            var id = Guid.NewGuid();
            var repository = new FakeResultRepository
            {
                Results = new List<ScrabbleScoreboard.Domain.Result>
                {
                    new() { Id = id },
                    new() { Id = id }
                }
            };
            var service = new ScrabbleScoreboard.Result.Api.Services.ResultService(repository);

            // Act & Assert

            try
            {
                await service.GetResultAsync(id);
                Assert.Fail("Expected duplicate matching results to throw.");
            }
            catch (InvalidOperationException)
            {
            }

            Assert.AreEqual(1, repository.GetAllCallCount);
        }

        [TestMethod]
        public async Task GetResult_Throws_When_Repository_Fails()
        {
            // Arrange

            var exception = new InvalidOperationException("Result repository failed.");
            var repository = new FakeResultRepository { GetAllException = exception };
            var service = new ScrabbleScoreboard.Result.Api.Services.ResultService(repository);

            // Act & Assert

            try
            {
                await service.GetResultAsync(Guid.NewGuid());
                Assert.Fail("Expected the repository exception to propagate.");
            }
            catch (InvalidOperationException thrownException)
            {
                Assert.AreSame(exception, thrownException);
            }

            Assert.AreEqual(1, repository.GetAllCallCount);
        }

        [TestMethod]
        public async Task GetLeaderboard_Returns_Entries_With_Ordered_Positions()
        {
            // Arrange

            var repository = new FakeResultRepository();
            var service = new ScrabbleScoreboard.Result.Api.Services.ResultService(repository);

            // Act

            var entries = (await service.GetLeaderboardAsync()).ToList();

            // Assert

            Assert.AreEqual(ScrabbleScoreboard.Result.Api.DemoData.DemoPlayers.Players.Count, entries.Count);
            CollectionAssert.AreEqual(Enumerable.Range(1, entries.Count).ToList(), entries.Select(entry => entry.Position).ToList());
            Assert.IsTrue(entries.All(entry => entry.GamesPlayed == ScrabbleScoreboard.Result.Api.DemoData.DemoPlayers.Players.Count - 1));
            Assert.IsTrue(entries.All(entry => entry.TotalScore > 0));
            Assert.IsTrue(entries.Zip(entries.Skip(1), (first, second) => first.TotalScore >= second.TotalScore).All(isOrdered => isOrdered));
            Assert.AreEqual(0, repository.GetAllCallCount);
        }

        [TestMethod]
        public async Task CreateResult_Returns_Repository_Result()
        {
            // Arrange

            var result = new ScrabbleScoreboard.Domain.Result
            {
                Id = Guid.NewGuid(),
                PlayerId = Guid.NewGuid(),
                Score = 0
            };

            var repository = new FakeResultRepository();
            var service = new ScrabbleScoreboard.Result.Api.Services.ResultService(repository);

            // Act

            var returnedResult = await service.CreateResultAsync(result);

            // Assert

            Assert.AreSame(result, returnedResult);
            Assert.AreSame(result, repository.AddedResult);
            Assert.AreEqual(1, repository.AddCallCount);
        }

        [TestMethod]
        public async Task CreateResult_Passes_Negative_Score_To_Repository()
        {
            // Arrange

            var result = new ScrabbleScoreboard.Domain.Result { Score = -1 };
            var repository = new FakeResultRepository();
            var service = new ScrabbleScoreboard.Result.Api.Services.ResultService(repository);

            // Act

            var returnedResult = await service.CreateResultAsync(result);

            // Assert

            Assert.AreSame(result, returnedResult);
            Assert.AreSame(result, repository.AddedResult);
            Assert.AreEqual(-1, repository.AddedResult!.Score);
            Assert.AreEqual(1, repository.AddCallCount);
        }

        [TestMethod]
        public async Task CreateResult_Throws_When_Repository_Fails()
        {
            // Arrange

            var result = new ScrabbleScoreboard.Domain.Result { Score = 10 };
            var exception = new InvalidOperationException("Result repository failed.");
            var repository = new FakeResultRepository { AddException = exception };
            var service = new ScrabbleScoreboard.Result.Api.Services.ResultService(repository);

            // Act & Assert

            try
            {
                await service.CreateResultAsync(result);
                Assert.Fail("Expected the repository exception to propagate.");
            }
            catch (InvalidOperationException thrownException)
            {
                Assert.AreSame(exception, thrownException);
            }

            Assert.AreSame(result, repository.AddedResult);
            Assert.AreEqual(1, repository.AddCallCount);
        }

        private sealed class FakeResultRepository : ScrabbleScoreboard.Result.Api.Persistence.Repositories.Interfaces.IResultRepository
        {
            public List<ScrabbleScoreboard.Domain.Result> Results { get; set; } = new();

            public Exception? GetAllException { get; set; }

            public Exception? AddException { get; set; }

            public ScrabbleScoreboard.Domain.Result? AddedResult { get; private set; }

            public int GetAllCallCount { get; private set; }

            public int AddCallCount { get; private set; }

            public Task<List<ScrabbleScoreboard.Domain.Result>> GetAllAsync()
            {
                GetAllCallCount++;

                if (GetAllException != null)
                {
                    return Task.FromException<List<ScrabbleScoreboard.Domain.Result>>(GetAllException);
                }

                return Task.FromResult(Results);
            }

            public Task<ScrabbleScoreboard.Domain.Result> AddAsync(ScrabbleScoreboard.Domain.Result result)
            {
                AddCallCount++;
                AddedResult = result;

                if (AddException != null)
                {
                    return Task.FromException<ScrabbleScoreboard.Domain.Result>(AddException);
                }

                return Task.FromResult(result);
            }
        }
    }
}
