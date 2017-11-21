using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchPM.Core;

namespace ArchPM.Data.UnitOfWorks
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Data.IUnitOfWork" />
    public class AdoUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Gets the transaction.
        /// </summary>
        /// <value>
        /// The transaction.
        /// </value>
        public IDbTransaction Transaction { get; private set; }
        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        public IDbContext DbContext { get; private set; }

        /// <summary>
        /// Gets or sets the after roll back action.
        /// </summary>
        /// <value>
        /// The after roll back action.
        /// </value>
        public Action<AdoUnitOfWork> AfterRollBackAction { get; set; }
        /// <summary>
        /// Gets or sets the after commit action.
        /// </summary>
        /// <value>
        /// The after commit action.
        /// </value>
        public Action<AdoUnitOfWork> AfterCommitAction { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdoUnitOfWork"/> class.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <param name="dbContext">The database context.</param>
        public AdoUnitOfWork(IDbTransaction transaction, IDbContext dbContext)
        {
            transaction.ThrowExceptionIfNull("transaction");
            dbContext.ThrowExceptionIfNull("dbContext");

            this.Transaction = transaction;
            this.DbContext = dbContext;

            this.AfterCommitAction = this.AfterRollBackAction = new Action<AdoUnitOfWork>(p => { });
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.Transaction == null)
                return;

            this.Transaction.Rollback();
            AfterRollBackAction(this);
            this.Transaction.Dispose();
            this.Transaction = null;
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        public void SaveChanges()
        {
            this.Transaction.ThrowExceptionIfNull(new InvalidOperationException("May not call save changes twice."));

            this.Transaction.Commit();
            AfterCommitAction(this);
            this.Transaction = null;
        }


    }
}
