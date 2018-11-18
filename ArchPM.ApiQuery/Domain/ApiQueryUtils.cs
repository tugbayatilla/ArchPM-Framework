using ArchPM.Core.Extensions;
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
        public static DbType ConvertFromSystemTypeToDbType(String name)
        {
            DbType result;

            if(Enum.IsDefined(typeof(DbType), name))
            {
                result = (DbType)Enum.Parse(typeof(DbType), name);
            }
            else
            {
                result = DbType.Object;
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
