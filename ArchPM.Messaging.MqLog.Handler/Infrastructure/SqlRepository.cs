using System;
using System.Data;
using System.Data.SqlClient;

namespace ArchPM.Messaging.MqLog.DbBusiness.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Messaging.MqLog.DbBusiness.Infrastructure.Repository" />
    public class SqlRepository : Repository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public SqlRepository(String connectionString)
        {
            this.ConnectionString = connectionString;
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Executes the specified SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="reader">The reader.</param>
        public void Execute(String sql, Action<IDataReader> reader)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    reader(command.ExecuteReader());
                }
            }
        }

        /// <summary>
        /// Executes the specified SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        public void Execute(String sql)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Executes the specified SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="command">The command.</param>
        public void Execute(String sql, IDbCommand command)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                command.Connection = connection;
                command.CommandText = sql;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                command.Dispose();
            }
        }


        /// <summary>
        /// Checks the table existance.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns></returns>
        public Boolean CheckTableExistance(string tableName)
        {
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();

                var dTable = conn.GetSchema("TABLES", new string[] { null, null, tableName });

                if (dTable.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
        }


    }

}
