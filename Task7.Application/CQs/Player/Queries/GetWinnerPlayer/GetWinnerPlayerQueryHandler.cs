using MediatR;
using Microsoft.EntityFrameworkCore;
using Task7.Application.Common.Constants;
using Task7.Application.Common.TicTacToe;
using Task7.Application.Interfaces;

namespace Task7.Application.CQs.Player.Queries.GetWinnerPlayer;

public class GetWinnerPlayerQueryHandler
    : IRequestHandler<GetWinnerPlayerQuery, string?>
{
    private readonly ITicTacToeDbContext _ticTacToeDbContext;
    private readonly ITicTacToe _ticTacToe;

    private const string Draw = "Draw";

    public GetWinnerPlayerQueryHandler(ITicTacToeDbContext ticTacToeDbContext,
        ITicTacToe ticTacToe)
    {
        _ticTacToeDbContext = ticTacToeDbContext;
        _ticTacToe = ticTacToe;
    }

    public async Task<string?> Handle(GetWinnerPlayerQuery request,
        CancellationToken cancellationToken)
    {
        var game = await _ticTacToeDbContext.Games
            .Include(g => g.Players)
            .FirstOrDefaultAsync(g => g.ConnectionId
                                      == request.ConnetionId, cancellationToken);
        if (game == null)
            throw new NullReferenceException($"The {nameof(game)} is null");

        var winnerChip = _ticTacToe.GetChipWinner(game.PlayingField);

        switch (winnerChip)
        {
            case GameChips.Cross or GameChips.Zero:
            {
                var winnerPlayer = game.Players
                    .First(p => p.GameChip == winnerChip);

                return winnerPlayer.Name;
            }
            case Winners.Draw:
                return Winners.Draw;
            default:
                return null;
        }
    }
}