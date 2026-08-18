using FluentValidation;

namespace Bookly.Application.Empleados.Commands.Create;

public sealed class CreateEmpleadoValidator : AbstractValidator<CreateEmpleadoCommand>
{
    public CreateEmpleadoValidator()
    {
        RuleFor(empleado => empleado.Nombre)
            .NotEmpty().WithMessage("El nombre no puede estar vacio")
            .MaximumLength(50).WithMessage("El nombre no puede superar los 50 caracteres");

        RuleFor(empleado => empleado.Apellido)
            .NotEmpty().WithMessage("El apellido no puede estar vacio")
            .MaximumLength(50).WithMessage("El apellido no puede superar los 50 caracteres");

        RuleFor(empleado => empleado.Telefono)
            .NotEmpty().WithMessage("El telefono no puede estar vacio")
            .Must(t => t.Count(char.IsDigit) is >= 9 and <= 15).WithMessage("El telefono debe tener entre 9 y 15 digitos")
            .Must(t => !t.Any(char.IsLetter)).WithMessage("El telefono no puede contener letras");

        RuleFor(empleado => empleado.Email)
            .NotEmpty().WithMessage("El email es requerido")
            .EmailAddress().WithMessage("El email no tiene un formato valido")
            .MaximumLength(100).WithMessage("El email no puede superar los 100 caracteres");
    }
}
