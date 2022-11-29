using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Task7.Application.CQs.Game.Command.Join;

public class JoinGameVm
{
    public Domain.Game Game { get; set; }
    public ModelStateDictionary ModelState { get; set; }
}