namespace Task7.Application.Hubs.Game;

public class GameInfoDto
{
    public string PlayerChip { get; set; }
    public string[] PlayingField { get; set; }
    public bool IsMove { get; set; }
}