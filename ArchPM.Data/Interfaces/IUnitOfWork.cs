using System;
using System.Data;

namespace ArchPM.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        IDbTransaction Transaction { get; }
        IDbContext DbContext { get; }
    }

   
}
