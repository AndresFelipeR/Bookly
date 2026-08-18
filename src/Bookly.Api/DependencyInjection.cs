using System.Text.Json.Serialization;
using Bookly.Api.Infrastructure.Exceptions;
using Bookly.Api.Swagger;

namespace Bookly.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        services.AddSwaggerDocumentation();
        services.AddEndpointsApiExplorer();

        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<ConflictExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
}
