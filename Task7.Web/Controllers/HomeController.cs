using Microsoft.AspNetCore.Mvc;
using Task7.Application.Interfaces;

namespace Task7.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}