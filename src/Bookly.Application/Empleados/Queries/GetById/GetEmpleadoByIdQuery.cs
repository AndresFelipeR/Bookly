using MediatR;

namespace Bookly.Application.Empleados.Queries.GetById;

public sealed record GetEmpleadoByIdQuery(Guid Id) : IRequest<GetEmpleadoByIdResponse>;
