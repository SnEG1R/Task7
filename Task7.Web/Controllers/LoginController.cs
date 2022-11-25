using Microsoft.AspNetCore.Mvc;

namespace Task7.Web.Controllers;

public class LoginController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}