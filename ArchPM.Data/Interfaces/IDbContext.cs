using System;
using System.Data;

namespace ArchPM.Data
{
    public interface IDbContext : IDisposable
    {

        //creates connection internally
        IUnitOfWork CreateUnitOfWork();
        IDbCommand CreateCommand();

    }
}
