using Bookly.Application.Common.Errors;
using Bookly.Application.Common.Exceptions;
using Bookly.Application.Common.Interfaces.Persistence;
using MediatR;

namespace Bookly.Application.Empleados.Commands.AsignarServicio;

public sealed class AsignarServicioEmpleadoCommandHandler
    : IRequestHandler<AsignarServicioEmpleadoCommand, AsignarServicioEmpleadoResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmpleadoRepository _empleadoRepository;
    private readonly IServicioRepository _servicioRepository;

    public AsignarServicioEmpleadoCommandHandler(
        IUnitOfWork unitOfWork,
        IEmpleadoRepository empleadoRepository,
        IServicioRepository servicioRepository)
    {
        _unitOfWork = unitOfWork;
        _empleadoRepository = empleadoRepository;
        _servicioRepository = servicioRepository;
    }

    public async Task<AsignarServicioEmpleadoResponse> Handle(
        AsignarServicioEmpleadoCommand request,
        CancellationToken cancellationToken)
    {
        var empleado = await _empleadoRepository.GetByIdWithServiciosAsync(
            request.EmpleadoId,
            cancellationToken);

        if (empleado is null)
            throw new NotFoundException(EmpleadoErrors.NotFound(request.EmpleadoId));

        var servicio = await _servicioRepository.GetByIdAsync(request.ServicioId, cancellationToken);
        if (servicio is null)
            throw new NotFoundException(ServicioErrors.NotFound(request.ServicioId));

        if (empleado.PuedeRealizarServicio(servicio))
            throw new ConflictException(EmpleadoErrors.ServicioYaAsignado(request.EmpleadoId, request.ServicioId));

        empleado.AsignarServicio(servicio);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AsignarServicioEmpleadoResponse(empleado.Id, servicio.Id);
    }
}
