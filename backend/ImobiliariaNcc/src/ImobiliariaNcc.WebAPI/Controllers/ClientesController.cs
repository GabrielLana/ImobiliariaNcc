using ImobiliariaNcc.Application.Modules.Clientes.Commands;
using ImobiliariaNcc.Application.Modules.Clientes.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImobiliariaNcc.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClientesController(IMediator _mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllClientesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetClienteQuery(id));
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateClienteCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Post), new { result }, result);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateClienteCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteClienteCommand(id));
        return Ok();
    }
}