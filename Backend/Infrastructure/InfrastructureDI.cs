using Domain.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class InfrastructureDI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NapaDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("NapaConnection"),
                    b => b.MigrationsAssembly(typeof(NapaDbContext).Assembly.FullName)));

            services.AddScoped<IShipRepository, ShipRepository>();
            services.AddScoped<IPortRepository, PortRepository>();
            services.AddScoped<IVoyageRepository, VoyageRepository>();

            return services;
        }
    }
}
