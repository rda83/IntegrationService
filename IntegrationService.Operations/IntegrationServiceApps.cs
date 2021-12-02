using Microsoft.Extensions.DependencyInjection;
using IntegrationService.MessageFormatManager;

namespace IntegrationService.Operations
{
    public static class IntegrationServiceApps
    {
        public static void AddIntegrationServiceOps(this IServiceCollection services)
        {
            services.AddTransient<IMessageFormatManager, MessageFormatManagerImpl>();
        }
    }
}
