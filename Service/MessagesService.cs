using System;
using IntegrationService.Data;
using IntegrationService.Models;
using IntegrationService.Models.Repositories;
using IntegrationService.Models.UpackageViewModel;
using IntegrationService.Service.Infrastructure.ReceivingSystem;

namespace IntegrationService.Service
{
    public class MessageService: IMessageService
    {
        #region constructor

            private readonly ISContext _context;
            private readonly IRouteMapRepository _routeMapRepository;
            private readonly IStatusRepository _statusRepository;
            private readonly IUpackageStatusRepository _upackageStatusRepository;
            private readonly IReceivingSystem _receivingSystem;

            public MessageService(ISContext context,
                IReceivingSystem receivingSystem,
                IRouteMapRepository routeMapRepository,
                IStatusRepository statusRepository,
                IUpackageStatusRepository upackageStatusRepository)
            {
                _context                    = context;
                _routeMapRepository         = routeMapRepository;
                _statusRepository           = statusRepository;
                _upackageStatusRepository   = upackageStatusRepository;
                _receivingSystem            = receivingSystem;
            }

        #endregion
        public ResponseUpackageViewModel PostUpackage(CreateUpackageViewModel viewModel){

            var timeShtamp = DateTime.UtcNow;

            var routeMap    = _routeMapRepository.GetRouteMap(viewModel.IntegrationId);
            var status      = _statusRepository.GetStatus(1); 
            
            var upackage        = new Upackage(viewModel.Data, timeShtamp, viewModel.IntegrationId, routeMap.SystemId);
            var upackageStatus  = new UpackageStatus(timeShtamp, status, upackage, "{}");
            
            _context.Upackages.Add(upackage);
            _context.UpackageStatuses.Add(upackageStatus);
            _context.SaveChanges();

            _receivingSystem.Send(routeMap.SystemId, viewModel.Data);

            var result = new ResponseUpackageViewModel();
            result.RequestId = upackage.Id;

            return result;
        }
        public UpackageCurrentStatusViewModel GetUpackageLastStatus(long id)
        {
            var currentStatus = _upackageStatusRepository.GetCurrentUpackageStatus(id);
            
            if (currentStatus == null)
            {
                return null;
            }

            var result = new UpackageCurrentStatusViewModel(id,
                    currentStatus.Date,
                    currentStatus.Status.Id,
                    currentStatus.Status.Presentation,
                    currentStatus.Message);

            return result;
        }
    }
}