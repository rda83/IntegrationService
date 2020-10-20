using IntegrationService.Models.UpackageViewModel;

namespace IntegrationService.Service
{
    public interface IMessageService
    {
        ResponseUpackageViewModel PostUpackage(CreateUpackageViewModel viewModel);
        UpackageCurrentStatusViewModel GetUpackageLastStatus(long id);
    }
}