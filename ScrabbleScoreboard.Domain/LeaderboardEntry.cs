namespace ScrabbleScoreboard.Domain;

/// <summary>
/// Represents a player's position on the leaderboard.
/// </summary>
public class LeaderboardEntry
{
    /// <summary>
    /// Identifier of the player.
    /// </summary>
    public Guid PlayerId { get; set; }

    /// <summary>
    /// Name of the player.
    /// </summary>
    public string PlayerName { get; set; } = string.Empty;

    /// <summary>
    /// Number of games played.
    /// </summary>
    public int GamesPlayed { get; set; }

    /// <summary>
    /// Total score accumulated across all games.
    /// </summary>
    public int TotalScore { get; set; }

    /// <summary>
    /// Current leaderboard position.
    /// </summary>
    public int Position { get; set; }
}

