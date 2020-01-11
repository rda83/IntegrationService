using Microsoft.EntityFrameworkCore;
using IntegrationService.Models;

namespace IntegrationService.Data
{
    public class UpackageContext : DbContext
    {
        public UpackageContext(DbContextOptions<UpackageContext> options)
            : base(options)
        {
        }

        public DbSet<Upackage> Upackages { get; set; }
    }
}