using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ArchPM.Core;
using ArchPM.Core.Extensions;
using ArchPM.Messaging.MqLog.Infrastructure;
using ArchPM.Core.Extensions.Advanced;

namespace ArchPM.Messaging.MqLog.DbBusiness.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Messaging.MqLog.DbBusiness.Infrastructure.IDbOperations" />
    public class SqlManager : IDbOperations
    {
        /// <summary>
        /// The repository
        /// </summary>
        readonly Repository repository;
        /// <summary>
        /// The configuration
        /// </summary>
        readonly MqLogDbConfig config;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="config">The configuration.</param>
        public SqlManager(Repository repository, MqLogDbConfig config)
        {
            this.BasicLog = new NullBasicLog();
            this.repository = repository;
            this.config = config;
        }

        public IBasicLog BasicLog { get; set; }

        /// <summary>
        /// Gets the name of the database.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// [SQL] No catalog in ConnectionString
        /// or
        /// [SQL] invalid catalog in ConnectionString
        /// </exception>
        String GetDatabaseName()
        {
            var list = this.repository.ConnectionString.Split(";".ToCharArray()).ToList();

            var dbCatalog = list.FirstOrDefault(p => p.ToLowerInvariant().Contains("initial catalog"));
            dbCatalog.ThrowExceptionIf(p => String.IsNullOrEmpty(p), new Exception("[SQL] No catalog in ConnectionString"));

            var splittedCatalog = dbCatalog.Split("=".ToCharArray());
            splittedCatalog.ThrowExceptionIf(p => p.Count() != 2, new Exception("[SQL] invalid catalog in ConnectionString"));

            var dbName = splittedCatalog[1];

            return dbName;
        }

        /// <summary>
        /// Creates the table if not exist.
        /// </summary>
        /// <param name="dto">The dto.</param>
        public void CreateTableIfNotExist(MqLogMessageDTO dto)
        {
            var tableName = TableNameCorrelation(dto.TBYEntityName);
            Boolean tableExist = TableExist(tableName);
            if (tableExist == false)
            {
                String createTableScript = GenerateCreateTableScript(dto);
                repository.Execute(createTableScript);

                this.BasicLog.Log(String.Format("[SQL] Table Created. {0} ", tableName));
            }
        }

        /// <summary>
        /// Creates the fields if not exist.
        /// </summary>
        /// <param name="dto">The dto.</param>
        public void CreateFieldsIfNotExist(MqLogMessageDTO dto)
        {
            //get table,
            //check every fields
            //find fields not exist
            //create them

            String tableName = TableNameCorrelation(dto.TBYEntityName);
            String script = String.Format("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{0}'", tableName);

            List<String> existingColumns = new List<String>();
            repository.Execute(script, (reader) =>
            {
                if (((SqlDataReader)reader).HasRows)
                {
                    while (reader.Read())
                    {
                        existingColumns.Add(reader.GetString(0));
                    }
                }
            });
            ///////////////////////////////////////////////////

            var allPropertiesInOnePlace = CollectAllPropertiesAsList(dto);
            List<PropertyDTO> fieldsNotExistInTable = new List<PropertyDTO>();
            foreach (var property in allPropertiesInOnePlace)
            {
                Boolean exist = existingColumns.Contains(property.Name);
                if (exist == false)
                {
                    fieldsNotExistInTable.Add(property);
                }
            }


            if (fieldsNotExistInTable.Count > 0)
            {
                ///////////////////////////////////////////////////
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("use {1}; ALTER TABLE {0} ", tableName, GetDatabaseName());
                sb.Append(" ADD ");
                foreach (var property in fieldsNotExistInTable)
                {
                    String sqlType = CLRConvertion(property.ValueType);
                    sqlType = sqlType.ToLower();
                    //String nullOrNot = property.Nullable ? "NULL" : "NOT NULL";

                    //sb.AppendFormat(" {0} AS {1} {2} ", property.Name, sqlType, nullOrNot);
                    sb.AppendFormat(" {0} {1} ", property.Name, sqlType);
                    sb.Append(" ,");
                }
                //remove last comma
                sb = sb.Remove(sb.Length - 1, 1);
                sb.Append(";");

                script = sb.ToString();
                ///////////////////////////////////////////////////
                repository.Execute(script);

                sb.Clear();
                sb.AppendFormat("[SQL] Fields created on {0}. ", tableName);
                foreach (var item in fieldsNotExistInTable)
                {
                    sb.AppendFormat("{0},", item.Name);
                }
                this.BasicLog.Log(sb.ToString());
            }
        }

        /// <summary>
        /// Persists the item.
        /// </summary>
        /// <param name="dto">The dto.</param>
        public void PersistItem(MqLogMessageDTO dto)
        {
            String script = CreateInsertScriptForCommand(dto);
            var command = CreateInsertCommand(dto);

            repository.Execute(script, command);
            this.BasicLog.Log(String.Format("[SQL] {0} DONE on {1}", dto.TBYMessageType, dto.TBYEntityName));
        }

        /// <summary>
        /// Called when [exception].
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public MqLogMessageDTO OnException(MqLogMessageDTO dto, Exception ex)
        {
            var exception = new GlobalMqLogException();
            exception.EntityName = dto.TBYEntityName;
            exception.ExceptionMessageString = ex.GetAllMessages();
            exception.MessageDtoString = dto.ToString();

            MqLogMessageDTO temp = new MqLogMessageDTO();
            temp.TBYMessageID = dto.TBYMessageID;
            temp.TBYMessageCreateTime = dto.TBYMessageCreateTime;
            temp.TBYMessageType = MqLogMessageTypes.Exception;
            temp.TBYEntityName = "TBYGLOBALEXCEPTION";
            temp.Properties = exception.Properties().ToList();

            return temp;
        }


        /// <summary>
        /// Generates the create table script.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        String GenerateCreateTableScript(MqLogMessageDTO dto)
        {
            StringBuilder sb = new StringBuilder();

            var tableName = TableNameCorrelation(dto.TBYEntityName);

            sb.AppendFormat("CREATE TABLE {0} (", tableName);

            var allPropertiesInOnePlace = CollectAllPropertiesAsList(dto);
            foreach (var property in allPropertiesInOnePlace)
            {
                String sqlType = CLRConvertion(property.ValueType);
                String nullOrNot = property.Nullable ? "NULL" : "NOT NULL";

                sb.AppendFormat(" {0} {1} {2} ,", property.Name, sqlType, nullOrNot);
            }
            sb = sb.Remove(sb.Length - 1, 1);

            sb.Append(" ); ");


            return sb.ToString();

        }

        /// <summary>
        /// Tables the exist.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns></returns>
        Boolean TableExist(string tableName)
        {
            String script = String.Format("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{0}'", tableName);
            repository.Execute(script, (r) =>
            {
                IDataReader reader = r;
            });

            Boolean result = repository.CheckTableExistance(tableName);
            if (!result)
                this.BasicLog.Log(String.Format("[SQL] Table is not exist: {0}", tableName));

            return result;
        }

        /// <summary>
        /// Colors the convertion.
        /// </summary>
        /// <param name="clrType">Type of the color.</param>
        /// <returns></returns>
        String CLRConvertion(String clrType)
        {
            String sqlDbType = clrType.ToSqlDbTypeAsString();
            return sqlDbType;
        }

        /// <summary>
        /// Tables the name correlation.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        String TableNameCorrelation(String name)
        {
            String tableName = String.Empty;
            if (!String.IsNullOrEmpty(config.TableNamePrefix))
                tableName = String.Concat(config.TableNamePrefix, "_");

            tableName = String.Concat(tableName, name);

            if (!String.IsNullOrEmpty(config.TableNameSuffix))
                tableName = String.Concat(tableName, "_", config.TableNameSuffix);

            return tableName;
        }

        /// <summary>
        /// Creates the insert script for command.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        String CreateInsertScriptForCommand(MqLogMessageDTO dto)
        {
            StringBuilder sb = new StringBuilder();

            var tableName = TableNameCorrelation(dto.TBYEntityName);
            var allPropertiesInOnePlace = CollectAllPropertiesAsList(dto);

            sb.AppendFormat("INSERT INTO {0} (", tableName);
            foreach (var property in allPropertiesInOnePlace)
            {
                sb.AppendFormat(" {0} ,", property.Name);
            }
            sb = sb.Remove(sb.Length - 1, 1);
            sb.Append(" ) ");

            sb.Append(" VALUES ( ");
            foreach (var property in allPropertiesInOnePlace)
            {
                sb.AppendFormat(" @{0} ,", property.Name);
            }
            sb = sb.Remove(sb.Length - 1, 1);
            sb.Append(" ); ");

            String script = sb.ToString();

            return script;
        }



        /// <summary>
        /// Creates the insert command.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        SqlCommand CreateInsertCommand(MqLogMessageDTO dto)
        {
            SqlCommand command = new SqlCommand();
            var allPropertiesInOnePlace = CollectAllPropertiesAsList(dto);
            foreach (var property in allPropertiesInOnePlace)
            {
                CreateCommandParameter(ref command, property);
            }

            return command;
        }


        /// <summary>
        /// Creates the command parameter.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="property">The property.</param>
        void CreateCommandParameter(ref SqlCommand cmd, PropertyDTO property)
        {
            var p = new SqlParameter();
            p.DbType = EnumManager<DbType>.TryParse(property.ValueType, DbType.String);
            p.ParameterName = property.Name;
            p.Value = property.Value == null ? DBNull.Value : property.Value;
            p.IsNullable = property.Nullable;


            //Kadir istegi uzerine eklenmistir. 05.02.2016
            if (property.ValueType == "DateTime"
                && (property.Value == null || property.Value == (Object)default(DateTime)))
            {
                p.Value = DBNull.Value;
            }

            cmd.Parameters.Add(p);
        }

        /// <summary>
        /// Collects all properties as list.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        List<PropertyDTO> CollectAllPropertiesAsList(MqLogMessageDTO dto)
        {
            var result = new List<PropertyDTO>();

            var staticProperties = dto.Properties();
            result.AddRange(staticProperties);

            //all fields must be set as null
            dto.Properties.ForEach(p => p.Nullable = true);

            result.AddRange(dto.Properties);

            return result;
        }




    }
}
