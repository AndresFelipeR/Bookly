using Bookly.Application.Common.Interfaces.Persistence;
using Bookly.Domain.Entities;
using Bookly.Domain.ValueObjects;
using MediatR;

namespace Bookly.Application.Empleados.Commands.Create;

public sealed class CreateEmpleadoCommandHandler : IRequestHandler<CreateEmpleadoCommand, CreateEmpleadoResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmpleadoRepository _empleadoRepository;

    public CreateEmpleadoCommandHandler(IUnitOfWork unitOfWork, IEmpleadoRepository empleadoRepository)
    {
        _unitOfWork = unitOfWork;
        _empleadoRepository = empleadoRepository;
    }

    public async Task<CreateEmpleadoResponse> Handle(CreateEmpleadoCommand request, CancellationToken cancellationToken)
    {
        var nombreCompleto = FullName.Create(request.Nombre, request.Apellido);
        var email = Email.Create(request.Email);
        var telefono = PhoneNumber.Create(request.Telefono);

        var empleado = Empleado.Create(nombreCompleto, email, telefono);

        await _empleadoRepository.AddAsync(empleado, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CreateEmpleadoResponse.FromEmpleado(empleado);
    }
}
