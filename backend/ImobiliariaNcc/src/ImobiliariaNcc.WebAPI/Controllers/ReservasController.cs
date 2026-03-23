using ImobiliariaNcc.Application.Modules.Reservas.Commands;
using ImobiliariaNcc.Application.Modules.Reservas.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImobiliariaNcc.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReservasController(IMediator _mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllReservasQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetReservaQuery(id));
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateReservaCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Post), new { result }, result);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateReservaCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteReservaCommand(id));
        return Ok();
    }
}