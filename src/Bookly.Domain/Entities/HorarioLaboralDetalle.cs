using Bookly.Domain.Enums;
using Bookly.Domain.Exceptions;

namespace Bookly.Domain.Entities;

public sealed class HorarioLaboralDetalle
{
    public Guid Id { get; private set; }
    public DiaSemana Dia { get; private set; }
    public TimeOnly HoraInicio { get; private set; }
    public TimeOnly HoraFin { get; private set; }

    private HorarioLaboralDetalle()
    {
        // Constructor privado para EF Core
    }

    private HorarioLaboralDetalle(DiaSemana dia, TimeOnly horaInicio, TimeOnly horaFin)
    {
        if (!Enum.IsDefined(dia))
            throw new DomainException("El día de la semana no es válido.");

        if (horaInicio >= horaFin)
            throw new DomainException("La hora de inicio debe ser anterior a la hora de fin.");

        Id = Guid.NewGuid();
        Dia = dia;
        HoraInicio = horaInicio;
        HoraFin = horaFin;
    }

    public static HorarioLaboralDetalle Create(DiaSemana dia, TimeOnly horaInicio, TimeOnly horaFin)
    {
        return new HorarioLaboralDetalle(dia, horaInicio, horaFin);
    }

    public bool SeSolapaCon(HorarioLaboralDetalle otro)
    {
        ArgumentNullException.ThrowIfNull(otro);

        if (Dia != otro.Dia)
            return false;

        return HoraInicio < otro.HoraFin && otro.HoraInicio < HoraFin;
    }
}
