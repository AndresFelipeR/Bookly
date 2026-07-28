using FluentValidation;

namespace Bookly.Application.Servicios.Commands.Create;

public sealed class CreateServicioValidator : AbstractValidator<CreateServicioCommand>
{
    public CreateServicioValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100).WithMessage("El nombre es requerido");
        RuleFor(x => x.Descripcion).NotEmpty().MaximumLength(1000).WithMessage("La descripción es requerida");
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.Currency).NotEmpty().MaximumLength(3).WithMessage("La moneda es requerida");
        RuleFor(x => x.Duracion).NotEmpty().GreaterThan(0).WithMessage("La duración es requerida");
        RuleFor(x => x.MargenCancelacion).NotEmpty().GreaterThan(0).WithMessage("El margen de cancelación es requerido");
        RuleFor(x => x.MargenAnticipacion).NotEmpty().GreaterThan(0).WithMessage("El margen de anticipación es requerido");
        RuleFor(x => x.TipoServicioId).NotEmpty().WithMessage("El tipo de servicio es requerido");

    }
}