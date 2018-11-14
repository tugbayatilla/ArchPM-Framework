using System;
using System.Collections.Generic;
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
    public interface IApiQueryDatabaseProvider
    {
        /// <summary>
        /// Gets or sets the name of the connection string.
        /// </summary>
        /// <value>
        /// The name of the connection string.
        /// </value>
        String ConnectionStringName { get; set; }
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        String ConnectionString { get; }
        /// <summary>
        /// Generates the command.
        /// </summary>
        /// <returns></returns>
        DbCommand GenerateCommand();
        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns></returns>
        DbConnection CreateConnection();
    }
}
