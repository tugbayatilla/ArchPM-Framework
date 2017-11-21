using ArchPM.Data;
using ArchPM.Data.UnitOfWorks;
using System.Collections.Generic;
using System.Data;

namespace ArchPM.Data.Contexts
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Data.IDbContext" />
    public class MemoryContext : IDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryContext"/> class.
        /// </summary>
        /// <param name="connectionName">Name of the connection.</param>
        public MemoryContext(string connectionName)
        {

        }

        /// <summary>
        /// The database
        /// </summary>
        public static List<dynamic> database = new List<dynamic>();

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        public static void Add<T>(T entity)
        {
            database.Add(entity);
        }


        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <returns></returns>
        public IDbCommand CreateCommand()
        {
            return null;
        }

        /// <summary>
        /// Creates the unit of work.
        /// </summary>
        /// <returns></returns>
        public IUnitOfWork CreateUnitOfWork()
        {
            return new MemoryUnitOfWork();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            
        }
    }
}
