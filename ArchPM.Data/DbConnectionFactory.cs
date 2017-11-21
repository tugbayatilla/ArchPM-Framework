using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

using ArchPM.Core;

namespace ArchPM.Data
{
    /// <summary>
    /// 
    /// </summary>
    internal class DbConnectionFactory
    {
        /// <summary>
        /// Creates and Opens the connection
        /// </summary>
        /// <param name="connectionName">Name of the connection.</param>
        /// <returns></returns>
        public static IDbConnection Create(String connectionName)
        {
            connectionName.ThrowExceptionIfNull("ConnectionName in AppConfigFactory cannot be null or empty! please provider a connection name!");

            var conStrSettings = ConfigurationManager.ConnectionStrings[connectionName];
            conStrSettings.ThrowExceptionIfNull(new ConfigurationErrorsException(String.Format("Failed to find connection string named '{0}' in config.", connectionName)));

            conStrSettings.ProviderName.ThrowExceptionIfNull("Failed to find ProviderName in config.");
            var providerFactory = DbProviderFactories.GetFactory(conStrSettings.ProviderName);
            providerFactory.ThrowExceptionIfNull(new ConfigurationErrorsException(String.Format("Failed to create factory with provider named '{0}' in config.", conStrSettings.ProviderName)));

            IDbConnection connection = providerFactory.CreateConnection();
            connection.ThrowExceptionIfNull(new ConfigurationErrorsException(String.Format("Failed to create a connection using the connectionstring named '{0}' in config.", connectionName)));

            connection.ConnectionString = conStrSettings.ConnectionString; 
            if(connection.State != ConnectionState.Open)
                connection.Open();
            return connection;
        }


    }
}
