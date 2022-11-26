using System.Security.Claims;
using MediatR;

namespace Task7.Application.CQs.User.Commands.Login;

public class LoginCommand : IRequest<ClaimsIdentity>
{
    public string Name { get; set; }
}