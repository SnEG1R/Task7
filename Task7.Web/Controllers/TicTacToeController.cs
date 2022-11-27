using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Task7.Web.Controllers;

[Authorize]
public class TicTacToeController : Controller
{
    [HttpGet]
    public IActionResult Index(Guid connectionId)
    {
        return View(connectionId);
    }
}