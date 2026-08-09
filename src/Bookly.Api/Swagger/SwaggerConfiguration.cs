using Microsoft.OpenApi;

namespace Bookly.Api.Swagger;

internal static class SwaggerConfiguration
{
    internal static void Configure(IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "Bookly API",
                    Version = "v1",
                    Description = "Api de gestion de reservas y servicios"
                });
        });
    }
}