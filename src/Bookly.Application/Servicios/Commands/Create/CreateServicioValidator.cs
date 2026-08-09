using FluentValidation;

namespace Bookly.Application.Servicios.Commands.Create;

public sealed class CreateServicioValidator : AbstractValidator<CreateServicioCommand>
{
    public CreateServicioValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre no puede estar vacio").MaximumLength(100).WithMessage("El nombre es requerido");
        RuleFor(x => x.Descripcion).NotEmpty().WithMessage("La descripcion no puede estar vacia").MaximumLength(1000).WithMessage("La descripción es requerida");
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.Currency).NotEmpty().MaximumLength(3).WithMessage("La moneda es requerida");
        RuleFor(x => x.Duracion).GreaterThan(0).WithMessage("La duración debe ser mayor a cero");
        RuleFor(x => x.MargenCancelacion).GreaterThan(0).WithMessage("El margen de cancelación es requerido");
        RuleFor(x => x.MargenAnticipacion).GreaterThan(0).WithMessage("El margen de anticipación es requerido");
        RuleFor(x => x.TipoServicioId).NotEmpty().WithMessage("El tipo de servicio es requerido");

    }
}