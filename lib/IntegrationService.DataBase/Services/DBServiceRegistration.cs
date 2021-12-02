using Microsoft.Extensions.DependencyInjection;

namespace IntegrationService.Data.Services
{
    public static class DBServiceRegistration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMessageFormatRepository, MessageFormatRepository>();
        }
    }
}
