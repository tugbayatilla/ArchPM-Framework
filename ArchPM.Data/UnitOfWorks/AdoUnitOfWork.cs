using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchPM.Core;

namespace ArchPM.Data.ADO
{
    public class AdoUnitOfWork : IUnitOfWork
    {
        public IDbTransaction Transaction { get; private set; }
        public IDbContext DbContext { get; private set; }

        public Action<AdoUnitOfWork> AfterRollBackAction { get; set; }
        public Action<AdoUnitOfWork> AfterCommitAction { get; set; }

        public AdoUnitOfWork(IDbTransaction transaction, IDbContext dbContext)
        {
            transaction.ThrowExceptionIfNull("transaction");
            dbContext.ThrowExceptionIfNull("dbContext");

            this.Transaction = transaction;
            this.DbContext = dbContext;

            this.AfterCommitAction = this.AfterRollBackAction = new Action<AdoUnitOfWork>(p => { });
        }

        public void Dispose()
        {
            if (this.Transaction == null)
                return;

            this.Transaction.Rollback();
            AfterRollBackAction(this);
            this.Transaction.Dispose();
            this.Transaction = null;
        }

        public void SaveChanges()
        {
            this.Transaction.ThrowExceptionIfNull(new InvalidOperationException("May not call save changes twice."));

            this.Transaction.Commit();
            AfterCommitAction(this);
            this.Transaction = null;
        }


    }
}
