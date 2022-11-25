using Microsoft.AspNetCore.Mvc;

namespace Task7.Web.Controllers;

public class ConnectionController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}