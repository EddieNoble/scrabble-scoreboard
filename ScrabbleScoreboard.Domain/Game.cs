namespace ScrabbleScoreboard.Domain;

/// <summary>
/// Represents a Scrabble game and the player results recorded for it.
/// </summary>
public class Game
{
    /// <summary>
    /// Unique identifier for the game.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Results submitted for the game.
    /// </summary>
    public ICollection<Result> Results { get; set; }
        = new List<Result>();

    /// <summary>
    /// Determines whether the game contains a valid number of players.
    /// </summary>
    /// <returns>
    /// True when the game contains between two and four player results; otherwise false.
    /// </returns>
    public bool IsValid()
    {
        return Results.Count >= 2
            && Results.Count <= 4;
    }
}
