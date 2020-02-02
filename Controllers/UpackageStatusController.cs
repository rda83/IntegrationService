using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntegrationService.Data;
using IntegrationService.Models;

namespace IntegrationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpackageStatusController : ControllerBase
    {
        private readonly ISContext _context;

        public UpackageStatusController(ISContext context)
        {
            _context = context;
        }

        // GET: api/UpackageStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UpackageStatus>>> GetUpackageStatuses()
        {
            return await _context.UpackageStatuses.ToListAsync();
        }

        // GET: api/UpackageStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UpackageStatus>> GetUpackageStatus(long id)
        {
            var upackageStatus = await _context.UpackageStatuses.FindAsync(id);

            if (upackageStatus == null)
            {
                return NotFound();
            }

            return upackageStatus;
        }

        // PUT: api/UpackageStatus/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUpackageStatus(long id, UpackageStatus upackageStatus)
        {
            if (id != upackageStatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(upackageStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UpackageStatusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UpackageStatus
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UpackageStatus>> PostUpackageStatus(UpackageStatus upackageStatus)
        {
            _context.UpackageStatuses.Add(upackageStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUpackageStatus", new { id = upackageStatus.Id }, upackageStatus);
        }

        // DELETE: api/UpackageStatus/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UpackageStatus>> DeleteUpackageStatus(long id)
        {
            var upackageStatus = await _context.UpackageStatuses.FindAsync(id);
            if (upackageStatus == null)
            {
                return NotFound();
            }

            _context.UpackageStatuses.Remove(upackageStatus);
            await _context.SaveChangesAsync();

            return upackageStatus;
        }

        private bool UpackageStatusExists(long id)
        {
            return _context.UpackageStatuses.Any(e => e.Id == id);
        }
    }
}
