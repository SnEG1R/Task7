using Microsoft.AspNetCore.Mvc;

namespace Task7.Web.Controllers;

public class MenuController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}