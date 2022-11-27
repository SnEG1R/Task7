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
            if (group.Players.Count <= 2 && group.Players.All(p => p.Name != playerName))
            {
                group.Players.Add(player);
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
        }

        await _ticTacToeDbContext.SaveChangesAsync(new CancellationToken());

        if (group.Players.Count >= 2)
            await Clients.Users(_ticTacToeDbContext.Players
                .Select(p => p.Name)).SendAsync("RemoveLoader");
    }
}