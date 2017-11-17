using System;
using System.Data;
using ArchPM.Core;

namespace ArchPM.Data.ADO
{
    public abstract class AdoContext : IDbContext
    {
        IDbConnection connection;
        IDbTransaction firstTransaction;
        readonly String connectionName;

        public AdoContext(String connectionName)
        {
            this.connectionName = connectionName;
        }

        /// <summary>
        /// Creates the unit of work with new transaction
        /// </summary>
        /// <returns></returns>
        public virtual IUnitOfWork CreateUnitOfWork()
        {
            //creates and opens connection
            if (this.connection == null)
                this.connection = DbConnectionFactory.Create(this.connectionName);

            // creates nulltransaction for dummy use
            // creates real transaction for the first time,
            // others must be dummy for nested transactions. all dummy transaction will be committed yet do nothing
            // and finally first transaction will be committed and commits all transactions
            IDbTransaction transaction = new NullTransaction(this.connection);
            if (firstTransaction == null)
            {
                transaction = this.connection.BeginTransaction();
                firstTransaction = transaction;
            }

            var uow = new AdoUnitOfWork(transaction, this)
            {
                AfterCommitAction = p =>
                {
                //clear if first transaction committed
                if (ReferenceEquals(firstTransaction, transaction))
                    {
                        firstTransaction = null;
                        if (this.connection != null)
                        {
                            this.connection.Close();
                            this.connection.Dispose();
                            this.connection = null;
                        }
                    }
                }
            };


            return uow;
        }

        /// <summary>
        /// Creates the command with first transaction
        /// </summary>
        /// <returns></returns>
        public virtual IDbCommand CreateCommand()
        {
            //creates and opens connection fistan: neden bir daha connection olusturuluyor? unitofwork olmadan command yaratmak icin mi?
            if (this.connection == null)
                this.connection = DbConnectionFactory.Create(this.connectionName);

            var cmd = this.connection.CreateCommand();
            if (firstTransaction != null)
            {
                firstTransaction.Connection.ThrowExceptionIfNull("Transaction is already committed! Connection null!");
                cmd.Transaction = firstTransaction;
            }
            return cmd;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.connection != null)
            {
                if(this.connection.State != ConnectionState.Closed)
                {
                    this.connection.Close();
                }
                this.connection.Dispose(); //fistan: unitofwork dispose olduktan sonra context ile is bitince onu da dispose etmeliyiz.!
                this.connection = null;
            }
        }
    }
}
