using System;

namespace IntegrationService.Data.Services
{
    public abstract class CommonRepository
    {
        protected readonly DBContext _context;

        public CommonRepository(DBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }
        
        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
