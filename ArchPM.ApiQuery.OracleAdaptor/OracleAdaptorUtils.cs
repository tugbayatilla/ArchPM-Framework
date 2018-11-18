using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery
{
    /// <summary>
    /// 
    /// </summary>
    public static class OracleAdaptorUtils
    {
        /// <summary>
        /// Converts the type of from system type to oracle database.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static OracleDbType ConvertFromSystemTypeToOracleDbType(String name)
        {
            var dbType = (DbType)Enum.Parse(typeof(DbType), name);
            var result = ConvertDbTypeToOracleDbType(dbType);
            return result;
        }


        public static OracleDbType ConvertDbTypeToOracleDbType(DbType dbType)
        {
            OracleDbType result = OracleDbType.Varchar2;

            switch (dbType)
            {
                case DbType.AnsiString:
                    result = OracleDbType.NVarchar2;
                    break;
                case DbType.Binary:
                    result = OracleDbType.LongRaw;
                    break;
                case DbType.Byte:
                    result = OracleDbType.Byte;
                    break;
                case DbType.Boolean:
                    result = OracleDbType.Int32; //not supported
                    break;
                case DbType.Currency:
                    result = OracleDbType.Decimal; //not supported
                    break;
                case DbType.Date:
                    result = OracleDbType.Date;
                    break;
                case DbType.DateTime:
                    result = OracleDbType.TimeStamp;
                    break;
                case DbType.Decimal:
                    result = OracleDbType.Decimal;
                    break;
                case DbType.Double:
                    result = OracleDbType.Double;
                    break;
                case DbType.Guid:
                    result = OracleDbType.NVarchar2; //not supported
                    break;
                case DbType.Int16:
                    result = OracleDbType.Int16;
                    break;
                case DbType.Int32:
                    result = OracleDbType.Int32;
                    break;
                case DbType.Int64:
                    result = OracleDbType.Int64;
                    break;
                case DbType.Object:
                    result = OracleDbType.RefCursor;
                    break;
                case DbType.SByte:
                    result = OracleDbType.Int64; //not supported
                    break;
                case DbType.Single:
                    result = OracleDbType.Single;
                    break;
                case DbType.String:
                    result = OracleDbType.Varchar2;
                    break;
                case DbType.Time:
                    result = OracleDbType.TimeStamp;
                    break;
                case DbType.UInt16:
                    result = OracleDbType.Int32; //not supported
                    break;
                case DbType.UInt32:
                    result = OracleDbType.Int64; //not supported
                    break;
                case DbType.UInt64:
                    result = OracleDbType.Double; //not supported
                    break;
                case DbType.VarNumeric:
                    result = OracleDbType.Double; //not supported
                    break;
                case DbType.AnsiStringFixedLength:
                    result = OracleDbType.Varchar2; //not supported
                    break;
                case DbType.StringFixedLength:
                    result = OracleDbType.Varchar2; //not supported
                    break;
                case DbType.Xml:
                    result = OracleDbType.Varchar2; //not supported
                    break;
                case DbType.DateTime2:
                    result = OracleDbType.TimeStamp; //not supported
                    break;
                case DbType.DateTimeOffset:
                    result = OracleDbType.TimeStamp; //not supported
                    break;
                default:
                    result = OracleDbType.RefCursor; //not supported
                    break;
            }


            return result;
        }


    }


}
