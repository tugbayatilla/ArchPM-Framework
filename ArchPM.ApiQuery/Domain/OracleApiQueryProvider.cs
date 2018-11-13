//using Oracle.ManagedDataAccess.Client;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.Common;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ArchPM.ApiQuery
//{
//    public class OracleApiQueryProvider : IApiQueryDatabaseProvider
//    {
//        readonly OracleClientFactory factory = OracleClientFactory.Instance;
//        public string ConnectionString => ConfigurationManager.ConnectionStrings["ANKA_UAT"].ConnectionString;

//        public DbConnection CreateConnection()
//        {
//            return factory.CreateConnection();
//        }

//        public DbCommand GenerateCommand()
//        {
//            return factory.CreateCommand();
//        }
//    }
//}
