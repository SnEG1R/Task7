using Microsoft.AspNetCore.SignalR;
using Task7.Application.Interfaces;

namespace Task7.Application.Hubs.Player;

public class PlayerHub : Hub
{
    public async Task GetPlayerName()
    {
        var playerName = Context.User?.Identity?.Name!;

        await Clients.User(playerName).SendAsync("GetPlayerName", playerName);
    }
}