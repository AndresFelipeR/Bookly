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
    }
}
