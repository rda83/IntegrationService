// Текущий статус по ИД
namespace IntegrationService.Models.Repositories
{
    public interface IUpackageStatusRepository
    {
        UpackageStatus GetCurrentUpackageStatus(long upackageStatusId);
    }
}