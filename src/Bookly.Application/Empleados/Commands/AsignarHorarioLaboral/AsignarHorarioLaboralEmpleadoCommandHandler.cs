using Bookly.Application.Common.Errors;
using Bookly.Application.Common.Exceptions;
using Bookly.Application.Common.Interfaces.Persistence;
using MediatR;

namespace Bookly.Application.Empleados.Commands.AsignarHorarioLaboral;

public sealed class AsignarHorarioLaboralEmpleadoCommandHandler
    : IRequestHandler<AsignarHorarioLaboralEmpleadoCommand, AsignarHorarioLaboralEmpleadoResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmpleadoRepository _empleadoRepository;
    private readonly IHorarioLaboralRepository _horarioLaboralRepository;

    public AsignarHorarioLaboralEmpleadoCommandHandler(
        IUnitOfWork unitOfWork,
        IEmpleadoRepository empleadoRepository,
        IHorarioLaboralRepository horarioLaboralRepository)
    {
        _unitOfWork = unitOfWork;
        _empleadoRepository = empleadoRepository;
        _horarioLaboralRepository = horarioLaboralRepository;
    }

    public async Task<AsignarHorarioLaboralEmpleadoResponse> Handle(
        AsignarHorarioLaboralEmpleadoCommand request,
        CancellationToken cancellationToken)
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

        if (empleado.TieneHorarioLaboral(horarioLaboral))
            throw new ConflictException(
                EmpleadoErrors.HorarioLaboralYaAsignado(request.EmpleadoId, request.HorarioLaboralId));

        empleado.AsignarHorarioLaboral(horarioLaboral, request.FechaInicio, request.FechaFin);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AsignarHorarioLaboralEmpleadoResponse(empleado.Id, horarioLaboral.Id);
    }
}
