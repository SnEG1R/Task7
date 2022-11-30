using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Task7.Application.Common.Constants;
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
        var player = await _tacToeDbContext.Players
            .FirstOrDefaultAsync(p => p.Name == request.PlayerName, cancellationToken);
        if (player == null)
            throw new NullReferenceException($"{nameof(player)} is null");

        Guid.TryParse(request.ConnectionId, out var connectionId);

        var game = await _tacToeDbContext.Games
            .Include(p => p.Players)
            .FirstOrDefaultAsync(g => g.ConnectionId
                                      == connectionId, cancellationToken);

        if (request.IsMvc)
        {
            if (game == null)
            {
                request.ModelState.AddModelError("game-error", "This game does not exist");
                return new JoinGameVm() { ModelState = request.ModelState };
            }

            if (game.Players.Count >= 2)
            {
                request.ModelState.AddModelError("game-error", "Game session busy");
                game.ConnectionId = Guid.Empty;
                return new JoinGameVm() { Game = game, ModelState = request.ModelState };
            }
        }

        if (game == null)
            throw new NullReferenceException($"The {nameof(game)} is null");

        switch (game.Players.Count)
        {
            case <= 0:
                player.GameChip = GetPlayerChip(player, game);
                game.Players.Add(player);
                break;
            case < 2 when game.Players.All(p => p.Name != request.PlayerName):
                player.GameChip = GetPlayerChip(player, game);
                game.Players.Add(player);
                break;
        }

        await _tacToeDbContext.SaveChangesAsync(cancellationToken);

        return new JoinGameVm() { Game = game, ModelState = request.ModelState };
    }

    private static string GetPlayerChip(Domain.Player player, Domain.Game game)
    {
        return player.Name == game.PlayerNameStep
            ? GameChips.Cross
            : GameChips.Zero;
    }
}