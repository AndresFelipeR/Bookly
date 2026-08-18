using Bookly.Domain.Entities;
using Bookly.Domain.Enums;

namespace Bookly.Application.HorariosLaborales.Commands.Create;

public sealed record CreateHorarioLaboralDetalleResponse(
    Guid Id,
    DiaSemana Dia,
    TimeOnly HoraInicio,
    TimeOnly HoraFin);

public sealed record CreateHorarioLaboralResponse(
    Guid Id,
    string Nombre,
    string Descripcion,
    IReadOnlyList<CreateHorarioLaboralDetalleResponse> Detalles)
{
    public static CreateHorarioLaboralResponse FromHorarioLaboral(HorarioLaboral horarioLaboral)
    {
        return new CreateHorarioLaboralResponse(
            horarioLaboral.Id,
            horarioLaboral.Nombre,
            horarioLaboral.Descripcion,
            horarioLaboral.Detalles
                .OrderBy(d => d.Dia)
                .ThenBy(d => d.HoraInicio)
                .Select(d => new CreateHorarioLaboralDetalleResponse(
                    d.Id,
                    d.Dia,
                    d.HoraInicio,
                    d.HoraFin))
                .ToList());
    }
}
