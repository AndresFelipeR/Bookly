using Bookly.Application.Common.Errors;
using Bookly.Application.Common.Exceptions;
using Bookly.Application.Common.Interfaces.Persistence;
using MediatR;

namespace Bookly.Application.Empleados.Commands.QuitarServicio;

public sealed class QuitarServicioEmpleadoCommandHandler
    : IRequestHandler<QuitarServicioEmpleadoCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmpleadoRepository _empleadoRepository;
    private readonly IServicioRepository _servicioRepository;

    public QuitarServicioEmpleadoCommandHandler(
        IUnitOfWork unitOfWork,
        IEmpleadoRepository empleadoRepository,
        IServicioRepository servicioRepository)
    {
        _unitOfWork = unitOfWork;
        _empleadoRepository = empleadoRepository;
        _servicioRepository = servicioRepository;
    }

    public async Task Handle(QuitarServicioEmpleadoCommand request, CancellationToken cancellationToken)
    {
        var empleado = await _empleadoRepository.GetByIdWithServiciosAsync(
            request.EmpleadoId,
            cancellationToken);

        if (empleado is null)
            throw new NotFoundException(EmpleadoErrors.NotFound(request.EmpleadoId));

        var servicio = await _servicioRepository.GetByIdAsync(request.ServicioId, cancellationToken);
        if (servicio is null)
            throw new NotFoundException(ServicioErrors.NotFound(request.ServicioId));

        if (!empleado.PuedeRealizarServicio(servicio))
            throw new NotFoundException(EmpleadoErrors.ServicioNoAsignado(request.EmpleadoId, request.ServicioId));

        empleado.QuitarServicio(servicio);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
