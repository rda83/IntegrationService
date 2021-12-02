using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IntegrationService.Application
{
    static public class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
