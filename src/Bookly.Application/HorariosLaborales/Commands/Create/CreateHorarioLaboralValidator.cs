using FluentValidation;

namespace Bookly.Application.HorariosLaborales.Commands.Create;

public sealed class CreateHorarioLaboralValidator : AbstractValidator<CreateHorarioLaboralCommand>
{
    public CreateHorarioLaboralValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre no puede estar vacío")
            .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres");

        RuleFor(x => x.Descripcion)
            .MaximumLength(500).WithMessage("La descripción no puede superar los 500 caracteres");

        RuleFor(x => x.Detalles)
            .NotEmpty().WithMessage("El horario laboral debe tener al menos un detalle");

        RuleForEach(x => x.Detalles).ChildRules(detalle =>
        {
            detalle.RuleFor(d => d.Dia)
                .IsInEnum().WithMessage("El día de la semana no es válido");

            detalle.RuleFor(d => d)
                .Must(d => d.HoraInicio < d.HoraFin)
                .WithMessage("La hora de inicio debe ser anterior a la hora de fin");
        });
    }
}
