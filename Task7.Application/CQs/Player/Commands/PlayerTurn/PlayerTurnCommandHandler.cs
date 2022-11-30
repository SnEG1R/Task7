using MediatR;
using Microsoft.EntityFrameworkCore;
using Task7.Application.Interfaces;

namespace Task7.Application.CQs.Player.Commands.PlayerTurn;

public class PlayerTurnCommandHandler 
    : IRequestHandler<PlayerTurnCommand, Domain.Game>
{
    private readonly ITicTacToeDbContext _ticTacToeDbContext;

    public PlayerTurnCommandHandler(ITicTacToeDbContext ticTacToeDbContext)
    {
        _ticTacToeDbContext = ticTacToeDbContext;
    }

    public async Task<Domain.Game> Handle(PlayerTurnCommand request, 
        CancellationToken cancellationToken)
    {
        var player = await _ticTacToeDbContext.Players
            .FirstAsync(p => p.Name == request.PlayerName, cancellationToken);

        var game = await _ticTacToeDbContext.Games
            .Include(g => g.Players)
            .FirstAsync(g => g.ConnectionId == request.ConnectionId, cancellationToken);

        var playerChip = player.GameChip;
        game.PlayingField[request.PositionField] = playerChip;
        
        game.PlayerNameStep = game.PlayerNameStep == game.Players[0].Name
            ? game.Players[1].Name
            : game.Players[0].Name;

        await _ticTacToeDbContext.SaveChangesAsync(new CancellationToken());

        return game;
    }
}