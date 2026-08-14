using Bookly.Application.Clientes.Commands.Create;
using Bookly.Application.Clientes.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookly.Api.Controllers;
[ApiController]
[Route("api/clientes")]

public class ClientesController : ControllerBase
{
  private readonly IMediator _mediator;
  public ClientesController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateClienteCommand command, CancellationToken cancellationToken)
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
      var response = await _mediator.Send(new GetClienteByIdQuery(id),cancellationToken);
      return Ok(response);
  }
  
}