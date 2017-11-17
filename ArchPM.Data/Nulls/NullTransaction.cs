using ArchPM.Core;
using System.Data;

namespace ArchPM.Data.ADO
{
    public class NullTransaction : IDbTransaction
    {
        public NullTransaction(IDbConnection connection)
        {
            this.Connection = connection;
            this.Connection.ThrowExceptionIfNull("NullTransaction connection cannot be null!");
        }
        public IDbConnection Connection { get; private set; }

        public IsolationLevel IsolationLevel { get { return IsolationLevel.ReadCommitted; } }

        public void Commit()
        {

        }

        public void Dispose()
        {

        }

        public void Rollback()
        {

        }
    }
}
