using Bookly.Application.Empleados.Commands.AsignarHorarioLaboral;
using Bookly.Application.Empleados.Commands.AsignarServicio;
using Bookly.Application.Empleados.Commands.Create;
using Bookly.Application.Empleados.Commands.QuitarHorarioLaboral;
using Bookly.Application.Empleados.Commands.QuitarServicio;
using Bookly.Application.Empleados.Queries.GetById;
using Bookly.Application.Empleados.Queries.GetHorariosLaborales;
using Bookly.Application.Empleados.Queries.GetServicios;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookly.Api.Controllers;

[ApiController]
[Route("api/empleados")]
public class EmpleadosController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmpleadosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmpleadoCommand command, CancellationToken cancellationToken)
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
        var response = await _mediator.Send(new GetEmpleadoByIdQuery(id), cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id:guid}/servicios")]
    public async Task<IActionResult> GetServicios(Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetEmpleadoServiciosQuery(id), cancellationToken);
        return Ok(response);
    }

    [HttpPost("{id:guid}/servicios")]
    public async Task<IActionResult> AsignarServicio(
        Guid id,
        [FromBody] AsignarServicioRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new AsignarServicioEmpleadoCommand(id, request.ServicioId),
            cancellationToken);

        return Ok(response);
    }

    [HttpDelete("{id:guid}/servicios/{servicioId:guid}")]
    public async Task<IActionResult> QuitarServicio(
        Guid id,
        Guid servicioId,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new QuitarServicioEmpleadoCommand(id, servicioId), cancellationToken);
        return NoContent();
    }

    [HttpGet("{id:guid}/horarios-laborales")]
    public async Task<IActionResult> GetHorariosLaborales(Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetEmpleadoHorariosLaboralesQuery(id), cancellationToken);
        return Ok(response);
    }

    [HttpPost("{id:guid}/horarios-laborales")]
    public async Task<IActionResult> AsignarHorarioLaboral(
        Guid id,
        [FromBody] AsignarHorarioLaboralRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new AsignarHorarioLaboralEmpleadoCommand(id, request.HorarioLaboralId),
            cancellationToken);

        return Ok(response);
    }

    [HttpDelete("{id:guid}/horarios-laborales/{horarioLaboralId:guid}")]
    public async Task<IActionResult> QuitarHorarioLaboral(
        Guid id,
        Guid horarioLaboralId,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new QuitarHorarioLaboralEmpleadoCommand(id, horarioLaboralId),
            cancellationToken);
        return NoContent();
    }
}

public sealed record AsignarServicioRequest(Guid ServicioId);
public sealed record AsignarHorarioLaboralRequest(Guid HorarioLaboralId);
