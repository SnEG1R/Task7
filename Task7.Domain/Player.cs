using System.Diagnostics.CodeAnalysis;

namespace Task7.Domain;

public class Player
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string GameChip { get; set; } = "";

    public List<Game> Games { get; set; }
    public List<Group> Groups { get; set; }
}