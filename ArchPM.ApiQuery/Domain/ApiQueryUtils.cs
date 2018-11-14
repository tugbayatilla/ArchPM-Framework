using ArchPM.Core.Extensions;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApiQueryUtils
    {
        /// <summary>
        /// Gets the API query field attribute on class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ApiQueryFieldAttribute GetApiQueryFieldAttributeOnClass<T>()
        {
            Type type = typeof(T);
            if ((typeof(T).IsList()))
            {
                type = typeof(T).GetGenericArguments()[0];
            }
            var resAttribute = (ApiQueryFieldAttribute)type.GetCustomAttribute(typeof(ApiQueryFieldAttribute));
            return resAttribute;
        }

        /// <summary>
        /// Converts the type of from system type to oracle database.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static OracleDbType ConvertFromSystemTypeToOracleDbType(String name)
        {
            OracleDbType result = OracleDbType.Varchar2;
            try
            {
                if (name == "Decimal")
                {
                    result = OracleDbType.Decimal;
                }
                else if (name == "Double")
                {
                    result = OracleDbType.Double;
                }
                else if (name == "Int16")
                {
                    result = OracleDbType.Int16;
                }
                else if (name == "Int32")
                {
                    result = OracleDbType.Int32;
                }
                else if (name == "Int64")
                {
                    result = OracleDbType.Int64;
                }
                else if (name == "UInt16")
                {
                    result = OracleDbType.Int16;
                }
                else if (name == "UInt32")
                {
                    result = OracleDbType.Int32;
                }
                else if (name == "UInt64")
                {
                    result = OracleDbType.Int64;
                }
                else if (name == "Single")
                {
                    result = OracleDbType.Single;
                }
                else if (name == "Byte")
                {
                    result = OracleDbType.Byte;
                }
                else if (name == "DateTime")
                {
                    result = OracleDbType.Date;
                }
                else if (name == "Boolean")
                {
                    result = OracleDbType.Int32;
                }
                else if (name == "String")
                {
                    result = OracleDbType.Varchar2;
                }
            }
            catch
            {
                result = OracleDbType.Varchar2;
            }

            return result;
        }

        /// <summary>
        /// Gets the name of the procedure.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static String GetProcedureName(String text)
        {
            String result = text;

            var lastIndexOfDot = text.LastIndexOf('.');
            if (lastIndexOfDot != -1)
            {
                result = text.Substring(lastIndexOfDot + 1);
            }

            return result;
        }
    }
}
