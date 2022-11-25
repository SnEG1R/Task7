namespace Task7.Domain;

public class Game
{
    public long Id { get; set; }
    public string ConnectionId { get; set; }
    public string[,] PlayingField { get; set; }
    public string Status { get; set; }

    public List<Player> Players { get; set; }
}