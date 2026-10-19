namespace ScrabbleScoreboard.Domain;

public class LeaderboardEntry
{
    public Guid PlayerId { get; set; }

    public string PlayerName { get; set; } = string.Empty;

    public int GamesPlayed { get; set; }

    public int TotalScore { get; set; }

    public int Position { get; set; }
}

