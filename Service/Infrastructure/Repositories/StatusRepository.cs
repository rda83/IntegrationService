using IntegrationService.Models;
using IntegrationService.Models.Repositories;
using IntegrationService.Data;
using System.Linq;

namespace IntegrationService.Service.Infrastructure.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ISContext _context;
        
        public StatusRepository(ISContext context)
        {
            _context = context;
        }

        public Status GetStatus(long statusId)
        {
            Status result = _context.Statuses
                    .Where(b => b.Id == statusId)
                    .FirstOrDefault();

            return result;
        }
    }
}