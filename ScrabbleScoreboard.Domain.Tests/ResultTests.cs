namespace ScrabbleScoreboard.Domain.Tests
{
    [TestClass]
    public class ResultTests
    {
        [TestMethod]
        public void Result_With_Positive_Score_Is_Valid()
        {
            // Arrange

            var result = new Result { PlayerId = Guid.NewGuid(), Score = 9 };

            // Act

            var isValid = result.IsValid();

            // Assert

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Result_With_Negative_Score_Is_Invalid()
        {
            // Arrange

            var result = new Result { PlayerId = Guid.NewGuid(), Score = -1 };

            // Act

            var isValid = result.IsValid();

            // Assert

            Assert.IsFalse(isValid);
        }
    }
}
