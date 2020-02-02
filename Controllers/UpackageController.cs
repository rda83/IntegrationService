using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntegrationService.Data;
using IntegrationService.Models;
using IntegrationService.Models.UpackageViewModel;

namespace IntegrationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpackageController : ControllerBase
    {
        private readonly ISContext _context;

        public UpackageController(ISContext context)
        {
            _context = context;
        }

        // POST: api/Upackage
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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

            var responseUpackageViewModel = new ResponseUpackageViewModel();
            responseUpackageViewModel.RequestId = upackage.Id; 
            return CreatedAtAction("GetUpackage", new { id = upackage.Id }, responseUpackageViewModel);
        }


        // GET: api/Upackage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Upackage>> GetUpackage(long id)
        {
            var upackage = await _context.Upackages.FindAsync(id);

            if (upackage == null)
            {
                return NotFound();
            }

            return upackage;
        }

    }
}
