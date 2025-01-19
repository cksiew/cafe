using CMS.Application.Configurations;
using CMS.CommonLib.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CMS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            services.AddScoped<ApplicationConfiguration>();

            return services;
        }
    }
}
