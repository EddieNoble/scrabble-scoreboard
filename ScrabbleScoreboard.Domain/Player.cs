namespace ScrabbleScoreboard.Domain;

/// <summary>
/// Represents a player participating in Scrabble games.
/// </summary>
public class Player
{
    /// <summary>
    /// Unique identifier for the player.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Display name of the player.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Contact email address of the player.
    /// </summary>
    public string EmailAddress { get; set; } = string.Empty;
}
