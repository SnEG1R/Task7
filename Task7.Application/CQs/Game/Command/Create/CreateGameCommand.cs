using System.Security.Claims;
using MediatR;

namespace Task7.Application.CQs.Game.Command.Create;

public class CreateGameCommand : IRequest<Guid>
{
    public string PayerName { get; set; }
}