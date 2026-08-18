using MediatR;

namespace Bookly.Application.HorariosLaborales.Queries.GetById;

public sealed record GetHorarioLaboralByIdQuery(Guid Id) : IRequest<GetHorarioLaboralByIdResponse>;
