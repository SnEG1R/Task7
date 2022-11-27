using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace Task7.Application.Common.UserIdProviders;

public class NameUserIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        return connection.User.FindFirst(ClaimTypes.Name)?.Value!;
    }
}