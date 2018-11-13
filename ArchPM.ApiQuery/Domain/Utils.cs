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
    static class Utils
    {
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

        public static OracleDbType ConvertFromSystemTypeToOracleDbType(String name)
        {
            OracleDbType result = OracleDbType.Varchar2;
            try
            {
                if (name == "Decimal")
                {
                    result=  OracleDbType.Decimal;
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


    }
}
