using Microsoft.AspNetCore.Mvc;

namespace Task7.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}