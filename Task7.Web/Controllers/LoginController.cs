using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Task7.Application.CQs.User.Commands.Login;
using Task7.Web.Models;

namespace Task7.Web.Controllers;

public class LoginController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public LoginController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginVm model)
    {
        var command = _mapper.Map<LoginCommand>(model);
        var identity = await _mediator.Send(command);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));

        return RedirectToAction("Index", "Menu");
    }
}