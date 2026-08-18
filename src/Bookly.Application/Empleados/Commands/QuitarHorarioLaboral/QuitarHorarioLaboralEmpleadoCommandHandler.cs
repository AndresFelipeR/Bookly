using Bookly.Application.Common.Errors;
using Bookly.Application.Common.Exceptions;
using Bookly.Application.Common.Interfaces.Persistence;
using MediatR;

namespace Bookly.Application.Empleados.Commands.QuitarHorarioLaboral;

public sealed class QuitarHorarioLaboralEmpleadoCommandHandler
    : IRequestHandler<QuitarHorarioLaboralEmpleadoCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmpleadoRepository _empleadoRepository;
    private readonly IHorarioLaboralRepository _horarioLaboralRepository;

    public QuitarHorarioLaboralEmpleadoCommandHandler(
        IUnitOfWork unitOfWork,
        IEmpleadoRepository empleadoRepository,
        IHorarioLaboralRepository horarioLaboralRepository)
    {
        _unitOfWork = unitOfWork;
        _empleadoRepository = empleadoRepository;
        _horarioLaboralRepository = horarioLaboralRepository;
    }

    public async Task Handle(QuitarHorarioLaboralEmpleadoCommand request, CancellationToken cancellationToken)
    {
        var empleado = await _empleadoRepository.GetByIdWithHorariosLaboralesAsync(
            request.EmpleadoId,
            cancellationToken);

        if (empleado is null)
            throw new NotFoundException(EmpleadoErrors.NotFound(request.EmpleadoId));

        var horarioLaboral = await _horarioLaboralRepository.GetByIdAsync(
            request.HorarioLaboralId,
            cancellationToken);

        if (horarioLaboral is null)
            throw new NotFoundException(HorarioLaboralErrors.NotFound(request.HorarioLaboralId));

        if (!empleado.TieneHorarioLaboral(horarioLaboral))
            throw new NotFoundException(
                EmpleadoErrors.HorarioLaboralNoAsignado(request.EmpleadoId, request.HorarioLaboralId));

        empleado.QuitarHorarioLaboral(horarioLaboral);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
