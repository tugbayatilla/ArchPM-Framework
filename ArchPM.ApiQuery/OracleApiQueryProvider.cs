using ArchPM.Core;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.ApiQuery.IApiQueryDatabaseProvider" />
    public class OracleApiQueryProvider : IApiQueryDatabaseProvider
    {
        /// <summary>
        /// The factory
        /// </summary>
        readonly OracleClientFactory factory = OracleClientFactory.Instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="OracleApiQueryProvider"/> class.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        public OracleApiQueryProvider(String connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString
        {
            get
            {
                ConfigurationManager.ConnectionStrings[this.ConnectionStringName]
                    .ThrowExceptionIfNull("Database connection string is not valid!");
                return ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString;
            }
        }
        /// <summary>
        /// Gets or sets the name of the connection string.
        /// </summary>
        /// <value>
        /// The name of the connection string.
        /// </value>
        public string ConnectionStringName { get; set; }


        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns></returns>
        public DbConnection CreateConnection()
        {
            return factory.CreateConnection();
        }
        /// <summary>
        /// Generates the command.
        /// </summary>
        /// <returns></returns>
        public DbCommand GenerateCommand()
        {
            return factory.CreateCommand();
        }
    }
}
