using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Task7.Application.Interfaces;

namespace Task7.Application.CQs.Game.Command.Join;

public class JoinGameCommandHandler
    : IRequestHandler<JoinGameCommand, JoinGameVm>
{
    private readonly ITicTacToeDbContext _tacToeDbContext;

    public JoinGameCommandHandler(ITicTacToeDbContext tacToeDbContext)
    {
        _tacToeDbContext = tacToeDbContext;
    }

    public async Task<JoinGameVm> Handle(JoinGameCommand request,
        CancellationToken cancellationToken)
    {
        var payer = await _tacToeDbContext.Players
            .FirstOrDefaultAsync(p => p.Name == request.PlayerName, cancellationToken);
        if (payer == null)
            throw new NullReferenceException($"{nameof(payer)} is null");

        Guid.TryParse(request.ConnectionId, out var connectionId);

        var game = await _tacToeDbContext.Games
            .Include(p => p.Players)
            .FirstOrDefaultAsync(g => g.ConnectionId
                                      == connectionId, cancellationToken);

        if (game == null)
        {
            request.ModelState.AddModelError("game-error", "This game does not exist");
            return new JoinGameVm() { ModelState = request.ModelState };
        }

        if (game.Players.Count >= 2)
        {
            request.ModelState.AddModelError("game-error", "Game session busy");
            return new JoinGameVm() { ConnectionId = Guid.Empty, ModelState = request.ModelState };
        }

        game.Players.Add(payer);
        await _tacToeDbContext.SaveChangesAsync(cancellationToken);

        return new JoinGameVm() { ConnectionId = game.ConnectionId, ModelState = request.ModelState };
    }
}