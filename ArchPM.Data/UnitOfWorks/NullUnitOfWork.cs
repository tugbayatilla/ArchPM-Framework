using System.Data;

namespace ArchPM.Data.UnitOfWorks
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Data.IUnitOfWork" />
    public class NullUnitOfWork : IUnitOfWork
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
                return null;
            }
        }

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        /// <value>
        /// The transaction.
        /// </value>
        public IDbTransaction Transaction
        {
            get
            {
                return null;
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
