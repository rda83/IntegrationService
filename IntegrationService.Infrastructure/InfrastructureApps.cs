using IntegrationService.Application.PropertyMapping;
using IntegrationService.DataValidatorService;
using IntegrationService.PropertyCheckerService;
using IntegrationService.PropertyMappingService;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationService.Infrastructure
{
    public static class InfrastructureApps
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<IPropertyCheckerService, PropertyCheckerService.PropertyCheckerService>();
            services.AddTransient<IPropertyMappingService, PropertyMappingService.PropertyMappingService>(s => PropertyMappingServiceBuilder.Create().AddAllMappings().Build());
            services.AddSingleton<IJSONDataValidator, JSONSchemaValidator>();
        }
    }
}
