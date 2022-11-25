using System.Diagnostics.CodeAnalysis;

namespace Task7.Domain;

public class Player
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    public List<Game> PlayingFields { get; set; }
}