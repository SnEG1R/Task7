using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task7.Application.CQs.Game.Command.Create;

namespace Task7.Web.Controllers;

[Authorize]
public class MenuController : Controller
{
    private readonly IMediator _mediator;

    public MenuController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame()
    {
        var command = new CreateGameCommand()
        {
            PayerName = User.Identity!.Name!
        };
        var connectionId = await _mediator.Send(command);

        return Ok(connectionId.ToString());
    }
}