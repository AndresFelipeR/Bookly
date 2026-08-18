using Bookly.Application.Common.Interfaces.Persistence;
using Bookly.Domain.Entities;
using MediatR;

namespace Bookly.Application.HorariosLaborales.Commands.Create;

public sealed class CreateHorarioLaboralCommandHandler
    : IRequestHandler<CreateHorarioLaboralCommand, CreateHorarioLaboralResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHorarioLaboralRepository _horarioLaboralRepository;

    public CreateHorarioLaboralCommandHandler(
        IUnitOfWork unitOfWork,
        IHorarioLaboralRepository horarioLaboralRepository)
    {
        _unitOfWork = unitOfWork;
        _horarioLaboralRepository = horarioLaboralRepository;
    }

    public async Task<CreateHorarioLaboralResponse> Handle(
        CreateHorarioLaboralCommand request,
        CancellationToken cancellationToken)
    {
        var detalles = request.Detalles.Select(d =>
            HorarioLaboralDetalle.Create(d.Dia, d.HoraInicio, d.HoraFin));

        var horarioLaboral = HorarioLaboral.Create(request.Nombre, request.Descripcion, detalles);

        await _horarioLaboralRepository.AddAsync(horarioLaboral, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CreateHorarioLaboralResponse.FromHorarioLaboral(horarioLaboral);
    }
}
