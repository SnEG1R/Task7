using Task7.Application.Common.Constants;
using Task7.Application.Common.Extension;

namespace Task7.Application.Common.TicTacToe;

public class TicTacToe : ITicTacToe
{
    private const int MapSize = 3;

    public string? GetChipWinner(string[] playingField)
    {
        if (playingField.All(e => e != GameChips.Empty))
            return Winners.Draw;
        
        var map = playingField.ConvertToMatrix(MapSize);
        
        var searchResult = new List<string?>
        {
            SearchVerticalWinner(map),
            SearchHorizontalWinner(map),
            SearchDiagonalWinner(map)
        };

        var winner = searchResult
            .FirstOrDefault(r => r != null);

        return winner;
    }

    private string? SearchVerticalWinner(string[,] map)
    {
        for (var i = 0; i < MapSize; i++)
        {
            var column = map.GetColumn(i);
            var winner = GetChipPlayerWinner(column);

            if (winner != null)
                return winner;
        }

        return null;
    }

    private string? SearchHorizontalWinner(string[,] map)
    {
        for (var i = 0; i < MapSize; i++)
        {
            var row = map.GetRow(i);
            var winner = GetChipPlayerWinner(row);

            if (winner != null)
                return winner;
        }

        return null;
    }

    private string? SearchDiagonalWinner(string[,] map)
    {
        var principalDiagonal = map.GetDiagonal();
        var secondaryDiagonal = map.GetDiagonal(false);

        var winner = GetChipPlayerWinner(principalDiagonal);
        if (winner != null)
            return winner;

        winner = GetChipPlayerWinner(secondaryDiagonal);

        return winner;
    }

    private static string? GetChipPlayerWinner(string[] sequence)
    {
        if (sequence.All(e => e == GameChips.Cross))
            return GameChips.Cross;
        if (sequence.All(e => e == GameChips.Zero))
            return GameChips.Zero;

        return null;
    }
}