using ArchPM.Core;
using System.Data;

namespace ArchPM.Data.Contexts
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Data.IDbTransaction" />
    public class NullTransaction : IDbTransaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullTransaction"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public NullTransaction(IDbConnection connection)
        {
            this.Connection = connection;
            this.Connection.ThrowExceptionIfNull("connection cannot be null!");
        }
        /// <summary>
        /// Specifies the Connection object to associate with the transaction.
        /// </summary>
        public IDbConnection Connection { get; private set; }

        /// <summary>
        /// Specifies the <see cref="T:System.Data.IsolationLevel" /> for this transaction.
        /// </summary>
        public IsolationLevel IsolationLevel { get { return IsolationLevel.ReadCommitted; } }

        /// <summary>
        /// Commits the database transaction.
        /// </summary>
        public void Commit()
        {

        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// Rolls back a transaction from a pending state.
        /// </summary>
        public void Rollback()
        {

        }
    }
}
