using Bookly.Application.TipoServicios.Commands.Create;
using Bookly.Application.TipoServicios.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookly.Api.Controllers;
[ApiController]
[Route("api/tipos-servicio")]
public sealed class TiposServicioController : ControllerBase
{
    private readonly IMediator _mediator;

    public TiposServicioController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTipoServicioCommand command,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetTiposServicioByIdQuery(id), cancellationToken);
        return Ok(response);
    }
}