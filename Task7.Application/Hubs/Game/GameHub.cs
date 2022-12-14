using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Task7.Application.Common.Constants;
using Task7.Application.CQs.Game.Command.Join;
using Task7.Application.CQs.Game.Command.Leave;
using Task7.Application.CQs.Game.Command.Restart;
using Task7.Application.CQs.Game.Queries.GetAllGame;
using Task7.Application.CQs.Player.Commands.PlayerTurn;
using Task7.Application.CQs.Player.Queries.GetWinnerPlayer;
using Task7.Application.Interfaces;
using Task7.Domain;

namespace Task7.Application.Hubs.Game;

[Authorize]
public class GameHub : Hub
{
    private readonly ITicTacToeDbContext _ticTacToeDbContext;
    private readonly IMediator _mediator;

    public GameHub(ITicTacToeDbContext ticTacToeDbContext, IMediator mediator)
    {
        _ticTacToeDbContext = ticTacToeDbContext;
        _mediator = mediator;
    }

    public async Task Connect(Guid connectionId)
    {
        var playerName = Context.User?.Identity?.Name!;
        var command = new JoinGameCommand()
        {
            ConnectionId = connectionId.ToString(),
            PlayerName = playerName
        };

        var joinGameVm = await _mediator.Send(command);

        var query = new GetWinnerPlayerQuery() { ConnetionId = joinGameVm.Game.ConnectionId };
        var winnerPlayer = await _mediator.Send(query);

        if (joinGameVm.Game.Players.Count >= 2)
        {
            if (joinGameVm.Game.Players.Count <= 1
                && joinGameVm.Game.Status != GameStatuses.Completed)
                await SendAllGame();

            await Send(joinGameVm.Game);

            if (winnerPlayer != null)
                await SendWinner(joinGameVm.Game.Players, winnerPlayer);
        }
    }

    public async Task PlayerTurn(Guid connectionId, int positionField)
    {
        var playerName = Context.User?.Identity?.Name!;
        var playerTurnCommand = new PlayerTurnCommand()
        {
            ConnectionId = connectionId,
            PlayerName = playerName,
            PositionField = positionField
        };
        var game = await _mediator.Send(playerTurnCommand);

        var query = new GetWinnerPlayerQuery() { ConnetionId = game.ConnectionId };
        var winnerPlayer = await _mediator.Send(query);

        await Send(game);

        if (winnerPlayer != null)
            await SendWinner(game.Players, winnerPlayer);
    }

    public async Task Restart(Guid connectionId)
    {
        var restartGameCommand = new RestartGameCommand(connectionId);
        await _mediator.Send(restartGameCommand);

        await SendAllGame();
        await Connect(connectionId);
    }

    public async Task SendAllGame()
    {
        var getAllGameQuery = new GetAllGameQuery();
        var gameDtos = await _mediator.Send(getAllGameQuery);

        await Clients.All.SendAsync("GetAllGame", gameDtos);
    }

    public async Task LeaveGame(Guid connectionId)
    {
        var playerName = Context.User!.Identity!.Name!;
        var finishGameCommand = new LeaveGameCommand(connectionId, playerName);
        var game = await _mediator.Send(finishGameCommand);

        await Send(game);
        await SendAllGame();
    }

    private async Task Send(Domain.Game game)
    {
        foreach (var player in game.Players)
        {
            await Clients.Users(player.Name)
                .SendAsync("GetConnectionInfo", new GameInfoDto()
                {
                    PlayerNameStep = game.PlayerNameStep,
                    PlayerChip = player.GameChip,
                    PlayingField = game.PlayingField,
                    GameStatus = game.Status
                });
        }
    }

    private async Task SendWinner(IEnumerable<Domain.Player> players, string winnerPlayer)
    {
        foreach (var player in players)
            await Clients.Users(player.Name)
                .SendAsync("GetWinnerPlayer", winnerPlayer, player.Name == winnerPlayer);
    }
}