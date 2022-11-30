using MediatR;
using Microsoft.EntityFrameworkCore;
using Task7.Application.Common.Constants;
using Task7.Application.Interfaces;

namespace Task7.Application.CQs.Game.Command.Leave;

public class LeaveGameCommandHandler
    : IRequestHandler<LeaveGameCommand, Domain.Game>
{
    private readonly ITicTacToeDbContext _ticTacToeDbContext;

    public LeaveGameCommandHandler(ITicTacToeDbContext ticTacToeDbContext)
    {
        _ticTacToeDbContext = ticTacToeDbContext;
    }

    public async Task<Domain.Game> Handle(LeaveGameCommand request,
        CancellationToken cancellationToken)
    {
        var game = await _ticTacToeDbContext.Games
            .Include(g => g.Players)
            .FirstOrDefaultAsync(g => g.ConnectionId == request.ConnectionId,
                cancellationToken);

        if (game == null)
            throw new NullReferenceException($"The {game} is null");

        var removedPlayer = game.Players
            .First(p => p.Name == request.PlayerName);

        // game.Players.ForEach(player => player.GameChip = GameChips.Empty);

        game.Players.Remove(removedPlayer);

        if (game.Players.Count <= 0)
            game.Status = GameStatuses.Completed;

        await _ticTacToeDbContext.SaveChangesAsync(cancellationToken);

        return game;
    }
}