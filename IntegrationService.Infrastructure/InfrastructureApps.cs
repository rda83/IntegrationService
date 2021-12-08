using IntegrationService.PropertyCheckerService;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationService.Infrastructure
{
    public static class InfrastructureApps
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<IPropertyCheckerService, PropertyCheckerService.PropertyCheckerService>();
        }
    }
}
