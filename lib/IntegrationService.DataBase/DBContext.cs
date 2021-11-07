using IntegrationService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace IntegrationService.Data
{
    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        public DbSet<MessageFormat> MessageFormats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MessageFormat>()
                .HasData(new List<MessageFormat>()
                {
                        new MessageFormat(){Id = 1000, Name = "Upackage", Scheme = Resource.PredefinedFormatUpackage}
                });
        }
    }
}
