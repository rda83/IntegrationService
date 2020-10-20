namespace IntegrationService.Models.Repositories
{
    public interface IStatusRepository
    {
        Status GetStatus(long statusId);
    }
}