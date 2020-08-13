using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntegrationService.Data;
using IntegrationService.Models;
using IntegrationService.Models.UpackageViewModel;
using IntegrationService.Service.ReceivingSystem;

namespace IntegrationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpackageController : ControllerBase
    {
        private readonly ISContext _context;
        private readonly IReceivingSystem _receivingSystem;

        public UpackageController(ISContext context, IReceivingSystem receivingSystem)
        {
            _context = context;
            _receivingSystem = receivingSystem;
        }

        // POST: api/Upackage
        [HttpPost]
        public async Task<ActionResult<ResponseUpackageViewModel>> PostUpackage(CreateUpackageViewModel viewModel)
        {

            RouteMap routeMap = _context.RouteMap
                    .Where(b => b.IntegrationId == viewModel.IntegrationId)
                    .FirstOrDefault();

            Status status = _context.Statuses
                    .Where(b => b.Id == 1)
                    .FirstOrDefault();

            var upackage = new Upackage();

            upackage.Data            = viewModel.Data;
            upackage.Date            = DateTime.UtcNow;
            upackage.IntegrationId   = viewModel.IntegrationId;
            upackage.SystemId        = routeMap.SystemId;

            var upackageStatus      = new UpackageStatus();
            upackageStatus.Date     = DateTime.UtcNow;
            upackageStatus.Status   = status;
            upackageStatus.Upackage = upackage;
            upackageStatus.Message  = "{}";
            

            _context.Upackages.Add(upackage);
            _context.UpackageStatuses.Add(upackageStatus);

            await _context.SaveChangesAsync();

            _receivingSystem.Send(routeMap.SystemId, viewModel.Data);

            var responseUpackageViewModel = new ResponseUpackageViewModel();
            responseUpackageViewModel.RequestId = upackage.Id; 
            return CreatedAtAction("GetUpackage", new { id = upackage.Id }, responseUpackageViewModel);
        }


        // GET: api/Upackage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UpackageCurrentStatusViewModel>> GetUpackage(long id)
        {
            //var upackage = await _context.Upackages.FindAsync(id);

            var currentStatus = _context.UpackageStatuses
                .Include(b => b.Status)
                .Where(b => b.UpackageId == id)
                .OrderByDescending(b => b.Date)
                .FirstOrDefault();

            if (currentStatus == null)
            {
                return NotFound();
            }

            var currentStatusView = new UpackageCurrentStatusViewModel();

            currentStatusView.UpackageId = id;
            currentStatusView.Date = currentStatus.Date;
            currentStatusView.StatusId = currentStatus.Status.Id;
            currentStatusView.Presentation = currentStatus.Status.Presentation;
            currentStatusView.Message = currentStatus.Message;

            return currentStatusView;
        }
    }
}
