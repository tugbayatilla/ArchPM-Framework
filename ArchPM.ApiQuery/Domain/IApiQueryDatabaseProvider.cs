using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery
{
    public interface IApiQueryDatabaseProvider
    {
        String ConnectionStringName { get; set; }
        String ConnectionString { get; }
        DbCommand GenerateCommand();
        DbConnection CreateConnection();
    }
}
