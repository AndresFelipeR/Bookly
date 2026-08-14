using FluentValidation;

namespace Bookly.Application.TipoServicios.Commands.Create;

public sealed class CreateTipoServicioValidator : AbstractValidator<CreateTipoServicioCommand>
{
    public CreateTipoServicioValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty() .MaximumLength(100)
            .WithMessage("El nombre es requerido y no puede superar los 100 caracteres.");
        RuleFor(x => x.Descripcion)
            .NotEmpty()
            .MaximumLength(500)
            .WithMessage("La descripción es requerida y no puede superar los 500 caracteres.");
    }
}