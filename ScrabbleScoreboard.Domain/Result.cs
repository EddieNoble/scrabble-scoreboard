namespace ScrabbleScoreboard.Domain;

public class Result
{
    public Guid Id { get; set; }

    public Guid PlayerId { get; set; }

    public int Score { get; set; }

    public bool IsValid()
    {
        return Score >= 0;
    }
}
