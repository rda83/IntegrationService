using IntegrationService.Models;
using IntegrationService.Models.Repositories;
using IntegrationService.Data;
using System.Linq;

namespace IntegrationService.Service.Infrastructure.Repositories
{
    public class RouteMapRepository : IRouteMapRepository
    {
        private readonly ISContext _context;
        
        public RouteMapRepository(ISContext context)
        {
            _context = context;
        }
        
        public RouteMap GetRouteMap(string integrationId)
        {
            RouteMap result = _context.RouteMap
                    .Where(b => b.IntegrationId == integrationId)
                    .FirstOrDefault();

            return result;
        }
    }
}