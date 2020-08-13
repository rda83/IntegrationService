using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using IntegrationService.Data;
using IntegrationService.Service;
using IntegrationService.Service.ReceivingSystem;


namespace IntegrationService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //string conString = Configuration.GetConnectionString("DatabaseSQLServer");
            string conString = Configuration.GetConnectionString("DatabasePGServer");
            
            /*
            services.AddDbContext<ISContext>(opt =>
               opt.UseSqlServer(conString));
            */
            services.AddDbContext<ISContext>(opt =>
                opt.UseNpgsql(conString));

            services.AddHostedService<ConsumeRabbitMQHostedService>();
            services.AddSingleton<IReceivingSystem, RabbitMQReceivingSystem>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
