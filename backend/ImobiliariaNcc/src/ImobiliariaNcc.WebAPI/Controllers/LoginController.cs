using ImobiliariaNcc.Application.Modules.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImobiliariaNcc.WebAPI.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class LoginController(IMediator _mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var loginResponse = await _mediator.Send(command);
        return Ok(loginResponse);
    }
}
