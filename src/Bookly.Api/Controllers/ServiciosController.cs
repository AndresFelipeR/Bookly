using Bookly.Application.Servicios.Commands.Create;
using Bookly.Application.Servicios.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookly.Api.Controllers;
[ApiController]
[Route("api/servicios")]
public sealed class ServiciosController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServiciosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateServicioCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if(result.IsFailure)
            return BadRequest(result.Errors);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Value.Id },
            result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetServicioByIdQuery(id), cancellationToken);
        if (result.IsFailure)
            return NotFound(result.Errors);

        return Ok(result.Value);
    }
}