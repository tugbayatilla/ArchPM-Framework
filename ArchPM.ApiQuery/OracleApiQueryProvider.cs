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
    public class OracleApiQueryProvider : IApiQueryDatabaseProvider
    {
        readonly OracleClientFactory factory = OracleClientFactory.Instance;

        public OracleApiQueryProvider(String connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
        }

        public string ConnectionString
        {
            get
            {
                ConfigurationManager.ConnectionStrings[this.ConnectionStringName]
                    .ThrowExceptionIfNull("Database connection string is not valid!");
                return ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString;
            }
        }
        public string ConnectionStringName { get; set; }


        public DbConnection CreateConnection()
        {
            return factory.CreateConnection();
        }
        public DbCommand GenerateCommand()
        {
            return factory.CreateCommand();
        }
    }
}
