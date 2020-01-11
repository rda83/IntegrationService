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
    public class RouteMapController : ControllerBase
    {
        private readonly ISContext _context;

        public RouteMapController(ISContext context)
        {
            _context = context;
        }

        // GET: api/RouteMap
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouteMap>>> GetRouteMap()
        {
            return await _context.RouteMap.ToListAsync();
        }

        // GET: api/RouteMap/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RouteMap>> GetRouteMap(string id)
        {
            var routeMap = await _context.RouteMap.FindAsync(id);

            if (routeMap == null)
            {
                return NotFound();
            }

            return routeMap;
        }

        // PUT: api/RouteMap/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRouteMap(string id, RouteMap routeMap)
        {
            if (id != routeMap.IntegrationId)
            {
                return BadRequest();
            }

            _context.Entry(routeMap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteMapExists(id))
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

        // POST: api/RouteMap
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<RouteMap>> PostRouteMap(RouteMap routeMap)
        {
            _context.RouteMap.Add(routeMap);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RouteMapExists(routeMap.IntegrationId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRouteMap", new { id = routeMap.IntegrationId }, routeMap);
        }

        // DELETE: api/RouteMap/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RouteMap>> DeleteRouteMap(string id)
        {
            var routeMap = await _context.RouteMap.FindAsync(id);
            if (routeMap == null)
            {
                return NotFound();
            }

            _context.RouteMap.Remove(routeMap);
            await _context.SaveChangesAsync();

            return routeMap;
        }

        private bool RouteMapExists(string id)
        {
            return _context.RouteMap.Any(e => e.IntegrationId == id);
        }
    }
}
