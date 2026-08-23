using FluentValidation;

namespace Bookly.Application.Empleados.Commands.AsignarHorarioLaboral;

public sealed class AsignarHorarioLaboralEmpleadoValidator
    : AbstractValidator<AsignarHorarioLaboralEmpleadoCommand>
{
    public AsignarHorarioLaboralEmpleadoValidator()
    {
        RuleFor(x => x.EmpleadoId)
            .NotEmpty().WithMessage("El empleado es requerido");

        RuleFor(x => x.HorarioLaboralId)
            .NotEmpty().WithMessage("El horario laboral es requerido");

        RuleFor(x => x.FechaInicio)
            .NotEmpty().WithMessage("La fecha de inicio es requerida");

        RuleFor(x => x.FechaFin)
            .GreaterThan(x => x.FechaInicio)
            .When(x => x.FechaFin.HasValue)
            .WithMessage("La fecha de fin debe ser posterior a la fecha de inicio");
    }
}
