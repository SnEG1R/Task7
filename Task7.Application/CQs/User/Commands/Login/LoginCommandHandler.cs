using System.Security.Claims;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Task7.Application.Interfaces;

namespace Task7.Application.CQs.User.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ClaimsIdentity>
{
    private readonly ITicTacToeDbContext _ticTacToeDbContext;

    public LoginCommandHandler(ITicTacToeDbContext ticTacToeDbContext)
    {
        _ticTacToeDbContext = ticTacToeDbContext;
    }

    public async Task<ClaimsIdentity> Handle(LoginCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _ticTacToeDbContext.Players
            .FirstOrDefaultAsync(u => u.Name == request.Name,
                cancellationToken: cancellationToken);

        if (user is null)
        {
            user = new Domain.Player()
            {
                Name = request.Name
            };

            await _ticTacToeDbContext.Players.AddAsync(user, cancellationToken);
            await _ticTacToeDbContext.SaveChangesAsync(cancellationToken);
        }

        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, user.Name),
        };

        var identity = new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

        return identity;
    }
}