using MediatR;

namespace Bookly.Application.Common.Behaviors;

public sealed class PerformanceBehavior<TRequest, Tresponse> 
    : IPipelineBehavior<TRequest,Tresponse> where TRequest : IRequest<Tresponse>
{
    public async Task<Tresponse> Handle(TRequest request, RequestHandlerDelegate<Tresponse> next, CancellationToken cancellationToken)
    {
        return await next();
    }
}