using App.Interfaces;
using App.Interfaces.Common;
using Infra.Context;
using Infra.Repositories;
using Infra.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services, IConfiguration configuration
        )
        {
            services.AddDbContext<BackendContext>(options =>
                options.UseInMemoryDatabase("Backend_InMemory")
            // options.UseSqlServer(
            // configuration.GetConnectionString("DefaultConnection"),
            // x => x.MigrationsHistoryTable("__EFMigrationHistory", null)
            // )
            );

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
