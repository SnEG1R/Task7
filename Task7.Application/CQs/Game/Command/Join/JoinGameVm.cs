using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Task7.Application.CQs.Game.Command.Join;

public class JoinGameVm
{
    public Guid ConnectionId { get; set; } = Guid.Empty;
    public ModelStateDictionary ModelState { get; set; }
}