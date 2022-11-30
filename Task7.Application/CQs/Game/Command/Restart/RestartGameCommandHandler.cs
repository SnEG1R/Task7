using MediatR;
using Microsoft.EntityFrameworkCore;
using Task7.Application.Common.Constants;
using Task7.Application.Interfaces;

namespace Task7.Application.CQs.Game.Command.Restart;

public class RestartGameCommandHandler
    : IRequestHandler<RestartGameCommand, Domain.Game>
{
    private readonly ITicTacToeDbContext _ticTacToeDbContext;

    public RestartGameCommandHandler(ITicTacToeDbContext ticTacToeDbContext)
    {
        _ticTacToeDbContext = ticTacToeDbContext;
    }

    public async Task<Domain.Game> Handle(RestartGameCommand request,
        CancellationToken cancellationToken)
    {
        var game = await _ticTacToeDbContext.Games
            .Include(g => g.Players)
            .FirstAsync(g => g.ConnectionId == request.ConnectionId, cancellationToken);

        // foreach (var player in game.Players)
        // {
        //     player.GameChip = player.Name == game.PlayerNameStep 
        //         ? GameChips.Cross 
        //         : GameChips.Zero;
        // }

        game.PlayingField = PlayingFields.Default;
        if (game.Players.Count >= 2)
            game.Players = new List<Domain.Player>();

        await _ticTacToeDbContext.SaveChangesAsync(new CancellationToken());

        return game;
    }
}