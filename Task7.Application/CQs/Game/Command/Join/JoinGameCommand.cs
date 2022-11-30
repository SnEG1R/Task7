using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Task7.Application.CQs.Game.Command.Join;

public class JoinGameCommand : IRequest<JoinGameVm>
{
    public string ConnectionId { get; set; }
    public string PlayerName { get; set; }
    public bool IsMvc { get; set; }
    public ModelStateDictionary ModelState { get; set; } = new();
}