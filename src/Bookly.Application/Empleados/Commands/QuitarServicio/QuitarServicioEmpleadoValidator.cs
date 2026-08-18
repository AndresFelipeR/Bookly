using FluentValidation;

namespace Bookly.Application.Empleados.Commands.QuitarServicio;

public sealed class QuitarServicioEmpleadoValidator : AbstractValidator<QuitarServicioEmpleadoCommand>
{
    public QuitarServicioEmpleadoValidator()
    {
        RuleFor(x => x.EmpleadoId)
            .NotEmpty().WithMessage("El empleado es requerido");

        RuleFor(x => x.ServicioId)
            .NotEmpty().WithMessage("El servicio es requerido");
    }
}
