using System.Security.Claims;
using MediatR;

namespace Task7.Application.CQs.Player.Commands.Login;

public class LoginCommand : IRequest<ClaimsIdentity>
{
    public string Name { get; set; }
}