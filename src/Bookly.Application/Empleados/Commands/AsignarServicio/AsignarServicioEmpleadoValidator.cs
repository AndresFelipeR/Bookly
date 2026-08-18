using FluentValidation;

namespace Bookly.Application.Empleados.Commands.AsignarServicio;

public sealed class AsignarServicioEmpleadoValidator : AbstractValidator<AsignarServicioEmpleadoCommand>
{
    public AsignarServicioEmpleadoValidator()
    {
        RuleFor(x => x.EmpleadoId)
            .NotEmpty().WithMessage("El empleado es requerido");

        RuleFor(x => x.ServicioId)
            .NotEmpty().WithMessage("El servicio es requerido");
    }
}
