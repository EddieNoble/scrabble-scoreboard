namespace ScrabbleScoreboard.Domain;

/// <summary>
/// Represents an individual player's score for a game.
/// </summary>
public class Result
{
    /// <summary>
    /// Unique identifier for the result.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Identifier of the player who achieved the score.
    /// </summary>
    public Guid PlayerId { get; set; }

    /// <summary>
    /// Scrabble score achieved by the player.
    /// </summary>
    public int Score { get; set; }

    public bool IsValid()
    {
        return Score >= 0;
    }
}
