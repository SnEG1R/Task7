namespace Task7.Domain;

public class Game
{
    public long Id { get; set; }
    public Guid ConnectionId { get; set; }
    public char[] PlayingField { get; set; }
    public string Status { get; set; }

    public List<Player> Players { get; set; }
}