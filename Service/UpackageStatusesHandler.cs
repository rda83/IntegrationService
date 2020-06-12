using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace IntegrationService.Service
{
    public class UpackageStatusesHandler : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int x = 0;
            while (!stoppingToken.IsCancellationRequested)
            {
                x++;
                Console.WriteLine(x);
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}