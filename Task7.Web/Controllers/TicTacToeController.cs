using Microsoft.AspNetCore.Mvc;

namespace Task7.Web.Controllers;

public class TicTacToeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}