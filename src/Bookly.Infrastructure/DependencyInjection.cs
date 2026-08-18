using Bookly.Application.Common.Interfaces.Persistence;
using Bookly.Application.Common.Interfaces.Persistence.Queries;
using Bookly.Infrastructure.Persistence;
using Bookly.Infrastructure.Persistence.Queries;
using Bookly.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bookly.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BooklyDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IServicioRepository, ServicioRepository>();
        services.AddScoped<ITipoServicioRepository, TipoServicioRepository>();
        services.AddScoped<IServicioQueries, ServicioQueries>();
        services.AddScoped<ITiposServicioQueries, TiposServicioQueries>();
        services.AddScoped<IClienteQueries, ClienteQueries>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IEmpleadoQueries, EmpleadoQueries>();
        services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
        services.AddScoped<IHorarioLaboralQueries, HorarioLaboralQueries>();
        services.AddScoped<IHorarioLaboralRepository, HorarioLaboralRepository>();
        return services;
    }

}
