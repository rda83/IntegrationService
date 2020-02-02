using Microsoft.EntityFrameworkCore;
using IntegrationService.Models;

namespace IntegrationService.Data
{
    public class ISContext : DbContext
    {
        public ISContext(DbContextOptions<ISContext> options)
            : base(options)
        {
        }

        public DbSet<Upackage> Upackages { get; set; }
        public DbSet<RouteMap> RouteMap { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<UpackageStatus> UpackageStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RouteMap>().HasKey(e => new{e.IntegrationId, e.SystemId});
        }
    }
}