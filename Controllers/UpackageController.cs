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
        private readonly UpackageContext _context;

        public UpackageController(UpackageContext context)
        {
            _context = context;
        }

        // POST: api/Upackage
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Upackage>> PostUpackage(CreateUpackageViewModel viewModel)
        {

           var upackage = new Upackage();

           upackage.Data            = viewModel.Data;
           upackage.Date            = DateTime.UtcNow;
           upackage.IntegrationId   = viewModel.IntegrationId;
           upackage.SystemId        = "d1a40efc-0688-4320-9d7f-e5eb96ef08a1";

            _context.Upackages.Add(upackage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUpackage", new { id = upackage.Id }, upackage);
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
