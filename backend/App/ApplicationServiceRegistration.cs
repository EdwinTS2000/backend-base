using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace App
{
    public static class ApplicationAssemblyMarker
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                // cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            });

            return services;
        }
    }
}