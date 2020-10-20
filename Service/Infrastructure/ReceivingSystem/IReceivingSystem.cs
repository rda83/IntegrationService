namespace IntegrationService.Service.Infrastructure.ReceivingSystem
{
    public interface IReceivingSystem
    {
        void Send(string key, string message);
    }
}