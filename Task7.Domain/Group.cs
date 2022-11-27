namespace Task7.Domain;

public class Group
{
    public long Id { get; set; }
    public Guid ConnectionId { get; set; }
    
    public List<Player> Players { get; set; }
}