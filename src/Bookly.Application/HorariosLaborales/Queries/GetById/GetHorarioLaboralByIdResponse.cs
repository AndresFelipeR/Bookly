using Bookly.Domain.Enums;

namespace Bookly.Application.HorariosLaborales.Queries.GetById;

public sealed record GetHorarioLaboralDetalleResponse(
    Guid Id,
    DiaSemana Dia,
    TimeOnly HoraInicio,
    TimeOnly HoraFin);

public sealed record GetHorarioLaboralByIdResponse(
    Guid Id,
    string Nombre,
    string Descripcion,
    IReadOnlyList<GetHorarioLaboralDetalleResponse> Detalles);
