using MediatR;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.EntityFrameworkCore;
using Task7.Application.Common.Constants;
using Task7.Application.Interfaces;
using Task7.Domain;

namespace Task7.Application.CQs.Game.Command.Create;

public class CreateGameCommandHandler
    : IRequestHandler<CreateGameCommand, Guid>
{
    private readonly ITicTacToeDbContext _ticTacToeDbContext;

    public CreateGameCommandHandler(ITicTacToeDbContext ticTacToeDbContext)
    {
        _ticTacToeDbContext = ticTacToeDbContext;
    }

    public async Task<Guid> Handle(CreateGameCommand request,
        CancellationToken cancellationToken)
    {
        var player = await _ticTacToeDbContext.Players
            .FirstOrDefaultAsync(p => p.Name == request.PayerName, cancellationToken);
        if (player == null)
            throw new NullReferenceException($"{nameof(player)} is null");
        
        var game = new Domain.Game()
        {
            Status = GameStatuses.Created,
            ConnectionId = Guid.NewGuid(),
            PlayingField = PlayingFields.Default,
            PlayerNameStep = player.Name
        };
        await _ticTacToeDbContext.Games.AddAsync(game, cancellationToken);
        await _ticTacToeDbContext.SaveChangesAsync(cancellationToken);

        return game.ConnectionId;
    }
}