using System;
using System.Data;
using ArchPM.Data;
using ArchPM.Data.Contexts;

namespace ArchPM.Data.UnitOfWorks
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Data.IUnitOfWork" />
    public class MemoryUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        public IDbContext DbContext
        {
            get
            {
                return new MemoryContext("");
            }
        }

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        /// <value>
        /// The transaction.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public IDbTransaction Transaction
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        public void SaveChanges()
        {
            
        }
    }
}
