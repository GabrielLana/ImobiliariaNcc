using ImobiliariaNcc.Application.Modules.Apartamentos.Commands;
using ImobiliariaNcc.Application.Modules.Apartamentos.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImobiliariaNcc.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ApartamentosController(IMediator _mediator) : ControllerBase
{
    [HttpGet("disponiveis")]
    public async Task<IActionResult> ListDisponiveis()
    {
        var result = await _mediator.Send(new ListApartamentosDisponiveisQuery());
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllApartamentosQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetApartamentoQuery(id));
        if(result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateApartamentoCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Post), new { result }, result);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateApartamentoCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteApartamentoCommand(id));
        return Ok();
    }
}
