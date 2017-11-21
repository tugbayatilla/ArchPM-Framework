using System;
using System.Data;

namespace ArchPM.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves the changes.
        /// </summary>
        void SaveChanges();
        /// <summary>
        /// Gets the transaction.
        /// </summary>
        /// <value>
        /// The transaction.
        /// </value>
        IDbTransaction Transaction { get; }
        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        IDbContext DbContext { get; }
    }

   
}
