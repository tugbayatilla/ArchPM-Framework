using ArchPM.Core;
using ArchPM.Core.Extensions;
using Oracle.ManagedDataAccess.Client;
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
    /// <seealso cref="ArchPM.ApiQuery.IApiQueryDatabaseAdaptor" />
    public class OracleApiQueryAdaptor : IApiQueryDatabaseAdaptor
    {
        /// <summary>
        /// The factory
        /// </summary>
        readonly OracleClientFactory factory = OracleClientFactory.Instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="OracleApiQueryAdaptor" /> class.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        public OracleApiQueryAdaptor(String connectionStringName)
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
        public DbConnection CreateDbConnection()
        {
            return factory.CreateConnection();
        }

        public DbDataAdapter CreateDbDataAdaptor(DbCommand command)
        {
            return new OracleDataAdapter(command as OracleCommand);
        }

        public DbParameter CreateDbParameter(string name, DbType dbType, ParameterDirection direction, object value, int size = 0)
        {
            OracleDbType oracleDbType = OracleAdaptorUtils.ConvertDbTypeToOracleDbType(dbType);
            var parameter = new OracleParameter()
            {
                ParameterName = name,
                OracleDbType = oracleDbType,
                Direction = direction,
                Value = value,
                Size = size
            };
            return parameter;
        }

        public DbParameter CreateDbParameter(string name, DbType dbType, ParameterDirection direction, int size = 0)
        {
            OracleDbType oracleDbType = OracleAdaptorUtils.ConvertDbTypeToOracleDbType(dbType);
            var parameter = new OracleParameter()
            {
                ParameterName = name,
                OracleDbType = oracleDbType,
                Direction = direction,
                Size = size
            };
            return parameter;
        }

        /// <summary>
        /// Generates the command.
        /// </summary>
        /// <returns></returns>
        public DbCommand CreateDbCommandCommand()
        {
            return factory.CreateCommand();
        }
    }
}
