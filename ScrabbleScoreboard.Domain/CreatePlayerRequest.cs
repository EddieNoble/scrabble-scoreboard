namespace ScrabbleScoreboard.Domain;

public class CreatePlayerRequest
{
    public string Name { get; set; }
    
    public string EmailAddress { get; set; }
}