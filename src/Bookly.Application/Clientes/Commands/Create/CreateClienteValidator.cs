using FluentValidation;

namespace Bookly.Application.Clientes.Commands.Create;

public sealed class CreateClienteValidator : AbstractValidator<CreateClienteCommand>
{
    public CreateClienteValidator()
    {
        RuleFor(cliente => cliente.Nombre)
        .NotEmpty().WithMessage("La nombre no puede estar vacio")
        .MaximumLength(50).WithMessage("La nombre no puede superar los 50 caracteres");

        RuleFor(cliente => cliente.Apellido)
        .NotEmpty().WithMessage("La apellido no puede estar vacio")
        .MaximumLength(50).WithMessage("La apellido no puede superar los 50 caracteres");
        
        RuleFor(cliente => cliente.Telefono)
        .NotEmpty().WithMessage("La telefono no puede estar vacio")
        .Must(t => t.Count(char.IsDigit) is >= 9 and <= 15).WithMessage("La telefono debe tener entre 9 y 15 digitos")
        .Must(t => !t.Any(char.IsLetter)).WithMessage("La telefono no puede contener letras");
        RuleFor(cliente => cliente.Email)
        .NotEmpty().WithMessage("El email es requerido")
        .EmailAddress().WithMessage("El email no tiene un formato valido")
        .MaximumLength(100).WithMessage("El email no puede superar los 100 caracteres");
    }
}