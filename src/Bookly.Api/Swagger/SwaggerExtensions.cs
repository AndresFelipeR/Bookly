namespace Bookly.Api.Swagger;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(
        this IServiceCollection services)
    {
        SwaggerConfiguration.Configure(services);
        return services;
    }
}