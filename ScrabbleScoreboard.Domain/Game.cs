namespace ScrabbleScoreboard.Domain;

public class Game
{
    public Guid Id { get; set; }

    public ICollection<Result> Results { get; set; }
        = new List<Result>();

    public bool IsValid()
    {
        return Results.Count >= 2
            && Results.Count <= 4;
    }
}
