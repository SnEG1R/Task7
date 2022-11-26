using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Task7.Application.CQs.Game.Command.Join;

public class JoinGameCommand : IRequest<ModelStateDictionary>
{
    public string ConnectionId { get; set; }
    public string PlayerName { get; set; }
    public ModelStateDictionary ModelState { get; set; }
}