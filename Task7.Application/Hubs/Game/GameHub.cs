using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Task7.Application.Common.Constants;
using Task7.Application.Interfaces;
using Task7.Domain;

namespace Task7.Application.Hubs.Game;

[Authorize]
public class GameHub : Hub
{
    private readonly ITicTacToeDbContext _ticTacToeDbContext;

    public GameHub(ITicTacToeDbContext ticTacToeDbContext)
    {
        _ticTacToeDbContext = ticTacToeDbContext;
    }

    public async Task Connect(Guid connectionId)
    {
        var playerName = Context.User?.Identity?.Name!;
        var game = await _ticTacToeDbContext.Games
            .FirstAsync(g => g.ConnectionId == connectionId);

        var group = await AddPlayerToGroup(playerName, connectionId);

        if (group.Players.Count >= 2)
            foreach (var playerItem in group.Players)
                await Clients.Users(playerItem.Name)
                    .SendAsync("GetConnectionInfo", new GameInfoDto()
                    {
                        PlayerChip = playerItem.GameChip,
                        PlayingField = game.PlayingField,
                        IsMove = playerItem.GameChip == GameChips.Cross
                    });
    }

    public async Task PlayerTurn(Guid connectionId, int positionField)
    {
        var playerName = Context.User?.Identity?.Name!;

        var player = await _ticTacToeDbContext.Players
            .FirstAsync(p => p.Name == playerName);
        var game = await _ticTacToeDbContext.Games
            .Include(g => g.Players)
            .FirstAsync(g => g.ConnectionId == connectionId);

        var playerChip = player.GameChip;
        game.PlayingField[positionField] = playerChip;

        await _ticTacToeDbContext.SaveChangesAsync(new CancellationToken());

        foreach (var playerItem in game.Players)
            await Clients.User(playerItem.Name)
                .SendAsync("GetPlayingField", new GameInfoDto()
                {
                    PlayerChip = playerItem.GameChip,
                    PlayingField = game.PlayingField,
                    IsMove = playerItem.GameChip != player.GameChip
                });
    }

    private async Task<Group> AddPlayerToGroup(string playerName, Guid connectionId)
    {
        var player = await _ticTacToeDbContext.Players
            .FirstAsync(p => p.Name == playerName);
        var group = await _ticTacToeDbContext.Groups
            .Include(g => g.Players)
            .FirstOrDefaultAsync(g => g.ConnectionId == connectionId);

        if (group != null)
        {
            if (group.Players.Count <= 2 && group.Players.All(p => p.Name != playerName))
            {
                player.GameChip = GameChips.Zero;
                group.Players.Add(player);
            }
        }
        else
        {
            player.GameChip = GameChips.Cross;
            group = new Group()
            {
                Players = new List<Player>() { player },
                ConnectionId = connectionId
            };
            await _ticTacToeDbContext.Groups.AddAsync(group);
        }

        await _ticTacToeDbContext.SaveChangesAsync(new CancellationToken());

        return group;
    }
}