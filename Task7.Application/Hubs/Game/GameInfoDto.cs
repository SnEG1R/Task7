namespace Task7.Application.Hubs.Game;

public class GameInfoDto
{
    public string PlayerNameStep { get; set; }
    public string PlayerChip { get; set; }
    public string[] PlayingField { get; set; }
    public bool IsGameFinish { get; set; }
}