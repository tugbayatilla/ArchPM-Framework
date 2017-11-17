using ArchPM.Data;
using ArchPM.Data.UnitOfWorks;
using System.Collections.Generic;
using System.Data;

namespace ArchPM.Data.Contexts
{
    public class MemoryContext : IDbContext
    {
        public MemoryContext(string connectionName)
        {

        }

        public static List<dynamic> database = new List<dynamic>();

        public static void Add<T>(T entity)
        {
            database.Add(entity);
        }


        public IDbCommand CreateCommand()
        {
            return null;
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            return new MemoryUnitOfWork();
        }

        public void Dispose()
        {
            
        }
    }
}
