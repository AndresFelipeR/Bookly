using Bookly.Application.Common.Interfaces.Persistence;
using Bookly.Domain.Entities;
using MediatR;

namespace Bookly.Application.TipoServicios.Commands.Create;

public sealed class CreateTipoServicioCommandHandler : IRequestHandler<CreateTipoServicioCommand, CreateTipoServicioResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITipoServicioRepository _tipoServicioRepository;

    public CreateTipoServicioCommandHandler(IUnitOfWork unitOfWork, ITipoServicioRepository tipoServicioRepository)
    {
        _unitOfWork = unitOfWork;
        _tipoServicioRepository = tipoServicioRepository;
    }

    public async Task<CreateTipoServicioResponse> Handle(CreateTipoServicioCommand request, CancellationToken cancellationToken)
    {
        var tipoServicio = TipoServicio.Create(
            request.Nombre,
            request.Descripcion);

        await _tipoServicioRepository.AddAsync(tipoServicio, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return CreateTipoServicioResponse.FromTipoServicio(tipoServicio);
    }
}