namespace IntegrationService.Service.ReceivingSystem
{
    public interface IReceivingSystem
    {
        void Send(string key, string message);
    }
}