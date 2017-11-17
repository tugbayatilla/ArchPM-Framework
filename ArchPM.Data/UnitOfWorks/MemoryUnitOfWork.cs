using System;
using System.Data;
using ArchPM.Data;
using ArchPM.Data.Contexts;

namespace ArchPM.Data.UnitOfWorks
{
    public class MemoryUnitOfWork : IUnitOfWork
    {
        public IDbContext DbContext
        {
            get
            {
                return new MemoryContext("");
            }
        }

        public IDbTransaction Transaction
        {
            get
            {
                throw new NotImplementedException();
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
