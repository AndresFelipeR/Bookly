using FluentValidation;

namespace Bookly.Application.Empleados.Commands.QuitarHorarioLaboral;

public sealed class QuitarHorarioLaboralEmpleadoValidator
    : AbstractValidator<QuitarHorarioLaboralEmpleadoCommand>
{
    public QuitarHorarioLaboralEmpleadoValidator()
    {
        RuleFor(x => x.EmpleadoId)
            .NotEmpty().WithMessage("El empleado es requerido");

        RuleFor(x => x.HorarioLaboralId)
            .NotEmpty().WithMessage("El horario laboral es requerido");
    }
}
