using Bookly.Domain.Exceptions;
namespace Bookly.Domain.Entities;

public sealed class EmpleadoHorarioLaboral
{
    public Guid EmpleadoId { get; private set; }
    public Empleado Empleado { get; private set; } = null!;
    public Guid HorarioLaboralId { get; private set; }
    public HorarioLaboral HorarioLaboral { get; private set; } = null!;
    public DateOnly FechaInicio { get; private set; }
    public DateOnly? FechaFin { get; private set; }

    private EmpleadoHorarioLaboral()
    {
        // Constructor privado para EF Core
    }

    private EmpleadoHorarioLaboral(Guid empleadoId, HorarioLaboral horarioLaboral, DateOnly fechaInicio, DateOnly? fechaFin)
    {
        ArgumentNullException.ThrowIfNull(horarioLaboral);

        if(empleadoId == Guid.Empty)
            throw new DomainException("El ID del empleado no puede ser vacío.");
        if(horarioLaboral.Id == Guid.Empty)
            throw new DomainException("El ID del horario laboral no puede ser vacío.");
        if(fechaInicio == DateOnly.MinValue)
            throw new DomainException("La fecha de inicio no puede ser vacía.");
        if(fechaFin != null && fechaFin < fechaInicio)
            throw new DomainException("La fecha de fin debe ser posterior a la fecha de inicio.");
        if(fechaFin != null && fechaFin > DateOnly.FromDateTime(DateTime.Now.AddYears(1)))
            throw new DomainException("La fecha de fin no puede ser más de un año desde la fecha de inicio.");

        EmpleadoId = empleadoId;
        HorarioLaboralId = horarioLaboral.Id;
        HorarioLaboral = horarioLaboral;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
    }

    public static EmpleadoHorarioLaboral Create(Guid empleadoId, HorarioLaboral horarioLaboral, DateOnly fechaInicio, DateOnly? fechaFin)
    {
        return new EmpleadoHorarioLaboral(empleadoId, horarioLaboral, fechaInicio, fechaFin);
    }
}
