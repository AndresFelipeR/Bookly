using FluentValidation;
using MediatR;
using ValidationException = Bookly.Application.Common.Exceptions.ValidationException;

namespace Bookly.Application.Common.Behaviors;

public class ValidationBehavior<TRequest,TResponse> : IPipelineBehavior<TRequest,TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    
    public  ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // 1. Validar si hay reglas configuradas sino saltamos al hanlder
        if (!_validators.Any())
        {
            return await next();
        }
        
        //2.Creamos el context
        var context = new ValidationContext<TRequest>(request);

        //3. Ejecutamos todos los validadores asincronamente
        var validationResult = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        
        //4. Extraemos los errores y agrupamos
        var failures = validationResult
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();
        
        //5.si hay errores que hacemos??
        if (failures.Count != 0)
        {
            var domainError = failures
                .Select(f => new Error(f.PropertyName, f.ErrorMessage))
                .ToList();
            // y ahora??
            
            throw new ValidationException(domainError);
        }
        
        return await next();
    }
}