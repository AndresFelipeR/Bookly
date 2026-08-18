using Bookly.Domain.Enums;
using MediatR;

namespace Bookly.Application.HorariosLaborales.Commands.Create;

public sealed record CreateHorarioLaboralDetalle(
    DiaSemana Dia,
    TimeOnly HoraInicio,
    TimeOnly HoraFin);

public sealed record CreateHorarioLaboralCommand(
    string Nombre,
    string Descripcion,
    IReadOnlyList<CreateHorarioLaboralDetalle> Detalles) : IRequest<CreateHorarioLaboralResponse>;
