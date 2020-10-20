using IntegrationService.Models;
using IntegrationService.Models.Repositories;
using IntegrationService.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace IntegrationService.Service.Infrastructure.Repositories
{
    public class UpackageStatusRepository : IUpackageStatusRepository
    {
        private readonly ISContext _context;
        
        public UpackageStatusRepository(ISContext context)
        {
            _context = context;
        }

        public UpackageStatus GetCurrentUpackageStatus(long upackageStatusId)
        {
            var result = _context.UpackageStatuses
                .Include(b => b.Status)
                .Where(b => b.UpackageId == upackageStatusId)
                .OrderByDescending(b => b.Date)
                .FirstOrDefault();

            return result;
        }
    }
}