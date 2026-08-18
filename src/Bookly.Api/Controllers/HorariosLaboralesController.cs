using Bookly.Application.HorariosLaborales.Commands.Create;
using Bookly.Application.HorariosLaborales.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookly.Api.Controllers;

[ApiController]
[Route("api/horarios-laborales")]
public sealed class HorariosLaboralesController : ControllerBase
{
    private readonly IMediator _mediator;

    public HorariosLaboralesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateHorarioLaboralCommand command,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(
            nameof(GetById),
            new { id = response.Id },
            response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetHorarioLaboralByIdQuery(id), cancellationToken);
        return Ok(response);
    }
}
