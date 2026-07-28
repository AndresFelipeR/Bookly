using Bookly.Application.Common;
using Bookly.Application.Common.Errors;
using Bookly.Application.Common.Interfaces.Persistence;
using Bookly.Domain.Entities;
using Bookly.Domain.ValueObjects;

namespace Bookly.Application.Servicios.Commands.Create;

public sealed class CreateServicioCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITipoServicioRepository _tipoServicioRepository;
    private readonly IServicioRepository _servicioRepository;

    public CreateServicioCommandHandler(
        IUnitOfWork unitOfWork,
        ITipoServicioRepository tipoServicioRepository,
        IServicioRepository servicioRepository)
    {
        _unitOfWork = unitOfWork;
        _tipoServicioRepository = tipoServicioRepository;
        _servicioRepository = servicioRepository;
    }

    public async Task<Result<CreateServicioResponse>> Handle(CreateServicioCommand command, CancellationToken cancellationToken)
    {
        var tipoServicio = await _tipoServicioRepository.GetByIdAsync(command.TipoServicioId, cancellationToken);
        if (tipoServicio is null)
        {
            return Result<CreateServicioResponse>.Failure(TipoServicioErrors.NotFound(command.TipoServicioId));
        }

        var precio = Money.Create(command.Amount, command.Currency);
        var duracion = Duracion.FromMinutes(command.Duracion);
        var politicaReserva = PoliticaReserva.Create(
            Duracion.FromMinutes(command.MargenCancelacion),
            Duracion.FromMinutes(command.MargenAnticipacion));

        var servicio = Servicio.Create(
            command.Nombre,
            command.Descripcion,
            precio,
            tipoServicio,
            duracion,
            politicaReserva);

        await _servicioRepository.AddAsync(servicio, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CreateServicioResponse>.Success(new CreateServicioResponse(
            servicio.Id,
            servicio.Nombre,
            servicio.Descripcion,
            servicio.Precio.Amount,
            servicio.Precio.Currency,
            servicio.TipoServicioId,
            servicio.Duracion.TotalMinutes,
            servicio.PoliticaReserva.MargenCancelacion.TotalMinutes,
            servicio.PoliticaReserva.MargenAnticipacion.TotalMinutes));
    }
}
