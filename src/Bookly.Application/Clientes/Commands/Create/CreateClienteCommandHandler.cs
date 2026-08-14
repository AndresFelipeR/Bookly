using Bookly.Application.Common.Interfaces.Persistence;
using Bookly.Domain.Entities;
using Bookly.Domain.ValueObjects;
using MediatR;

namespace Bookly.Application.Clientes.Commands.Create;

public sealed class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, CreateClienteResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClienteRepository _clienteRepository;

    public CreateClienteCommandHandler(IUnitOfWork unitOfWork, IClienteRepository clienteRepository)
    {
        _unitOfWork = unitOfWork;
        _clienteRepository = clienteRepository;
    }

    public async Task<CreateClienteResponse> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
    {
        var nombreCompleto = FullName.Create(request.Nombre, request.Apellido);
        var email = Email.Create(request.Email);
        var telefono = PhoneNumber.Create(request.Telefono);

        var cliente = Cliente.Create(
            nombreCompleto, email, telefono);
        
        await _clienteRepository.AddAsync(cliente,cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return CreateClienteResponse.FromCliente(cliente);
    }
}