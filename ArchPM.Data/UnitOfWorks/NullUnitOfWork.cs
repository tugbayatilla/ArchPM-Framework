using System.Data;

namespace ArchPM.Data.UnitOfWorks
{
    public class NullUnitOfWork : IUnitOfWork
    {
        public IDbContext DbContext
        {
            get
            {
                return null;
            }
        }

        public IDbTransaction Transaction
        {
            get
            {
                return null;
            }
        }

        public void Dispose()
        {
            
        }

        public void SaveChanges()
        {
            
        }
    }
}
