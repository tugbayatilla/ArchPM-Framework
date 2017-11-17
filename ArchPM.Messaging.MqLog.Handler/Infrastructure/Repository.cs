using System;
using System.Data;

namespace ArchPM.Messaging.MqLog.DbBusiness.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public interface Repository
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        String ConnectionString { get; }

        /// <summary>
        /// Executes the specified SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="reader">The reader.</param>
        void Execute(String sql, Action<IDataReader> reader);
        /// <summary>
        /// Executes the specified SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        void Execute(String sql);
        /// <summary>
        /// Executes the specified SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="command">The command.</param>
        void Execute(String sql, IDbCommand command);


        /// <summary>
        /// Checks the table existance.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns></returns>
        Boolean CheckTableExistance(String tableName);

    }
}
