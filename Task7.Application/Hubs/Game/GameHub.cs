using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Task7.Application.Interfaces;
using Task7.Domain;

namespace Task7.Application.Hubs.Game;

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

        var player = await _ticTacToeDbContext.Players
            .FirstAsync(p => p.Name == playerName);
        var group = await _ticTacToeDbContext.Groups
            .Include(g => g.Players)
            .FirstOrDefaultAsync(g => g.ConnectionId == connectionId);

        if (group != null)
        {
            if (group.Players.Count <= 2)
            {
                group.Players.Add(player);
                await SaveGroup(connectionId);
            }
        }
        else
        {
            group = new Group()
            {
                Players = new List<Player>() { player },
                ConnectionId = connectionId
            };
            await _ticTacToeDbContext.Groups.AddAsync(group);
            await SaveGroup(connectionId);

            return;
        }

        await Clients.Group(connectionId.ToString()).SendAsync("RemoveLoader");
    }

    private async Task SaveGroup(Guid connectionId)
    {
        await _ticTacToeDbContext.SaveChangesAsync(new CancellationToken());
        await Groups.AddToGroupAsync(Context.ConnectionId, connectionId.ToString());
    }
}