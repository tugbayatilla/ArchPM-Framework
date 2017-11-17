using ArchPM.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Core.Extensions.Advanced
{
    public static class AdvancedExtensions
    {
        #region Datetime

        /// <summary>
        /// Returns the number of milliseconds since Jan 1, 1970
        /// This method can be used for Json Datetime
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">dt</exception>
        public static Double ToJsonDateTime(this DateTime dt)
        {
            if (dt == DateTime.MinValue || dt == DateTime.MaxValue)
                throw new ArgumentOutOfRangeException("dt");

            DateTime startPointDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan ts = new TimeSpan(dt.Ticks - startPointDateTime.Ticks);
            return ts.TotalMilliseconds;
        }

        /// <summary>
        /// To the database acceptable.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static DateTime? ToSqlDbAcceptable(this DateTime? dt)
        {
            if (dt.HasValue)
            {
                return ToSqlDbAcceptable(dt.Value);
            }
            else
                return null;
        }

        /// <summary>
        /// To the database acceptable.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static DateTime? ToSqlDbAcceptable(this DateTime dt)
        {
            if (dt.Year <= 1900)
                return null;

            return dt;
        }

        /// <summary>
        /// To the database default string.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static String ToDbDefaultString(this DateTime dt)
        {
            if (dt == DateTime.MinValue || dt == DateTime.MaxValue)
                throw new ArgumentOutOfRangeException("dt");

            String result = String.Format("{0}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}",
                            dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            return result;
        }

        ///// <summary>
        ///// To the database default string.
        ///// </summary>
        ///// <param name="dt">The dt.</param>
        ///// <returns></returns>
        //public static String ToDbDefaultString(this DateTime? dt)
        //{
        //    dt.NotNull();

        //    return ToDbDefaultString(dt.Value);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static String ToShortDateString(this DateTime? dt, String defaultValue = "")
        {
            String result = defaultValue;
            if (dt.HasValue)
                result = dt.Value.ToShortDateString();

            return result;
        }

        #endregion


        #region assembly

        //todo:test yazilmali
        public static IEnumerable<T> GetProvider<T>(this Assembly currentAssembly, params Object[] constructorArguments)
        {
            var types = currentAssembly.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsClass) continue;
                if (type.GetInterfaces().Contains(typeof(T)) || type.BaseType.Name == typeof(T).Name)
                {
                    Type result = type;
                    if (type.ContainsGenericParameters)
                    {
                        result = type.MakeGenericType(typeof(T).GenericTypeArguments);
                    }

                    var provider = (T)Activator.CreateInstance(result, constructorArguments);

                    yield return provider;
                }
            }
        }

        public static IEnumerable<Type> GetProviderTypes<T>(this Assembly currentAssembly)
        {
            var types = currentAssembly.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsClass) continue;
                if (type.IsAbstract) continue;
                if (type.GetInterfaces().Contains(typeof(T)) || type.BaseType.Name == typeof(T).Name)
                {
                    yield return type;
                }
            }
        }

        /// <summary>
        /// Create and instance and cast
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="constructorArguments"></param>
        /// <returns></returns>
        public static T CreateInstanceAndCast<T>(this Type type, params Object[] constructorArguments)
        {
            if (type.ContainsGenericParameters)
            {
                type = type.MakeGenericType(typeof(T).GenericTypeArguments);
            }
            var instance = (T)Activator.CreateInstance(type, constructorArguments);
            return instance;
        }

        #endregion

        public static void AddProperty(this ExpandoObject expando, String propertyName, Object propertyValue)
        {
            var expandoDict = expando as IDictionary<String, Object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }

        #region list

       

        /// <summary>
        /// Tests if provided list of objects is not null or empty.
        /// Throws ValidationException if it is null or is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objects">list of objects</param>
        /// <param name="msg">The MSG.</param>
        /// <exception cref="ArchPM.Core.Exceptions.ValidationException"></exception>
        public static void NotEmpty<T>(this IList<T> objects, string msg)
        {
            if (objects == null || objects.Count == 0)
            {
                if (string.IsNullOrEmpty(msg))
                {
                    msg = "The list can't be empty";
                }
                throw new ValidationException(msg);
            }
        }


        /// <summary>
        /// Adds as unique.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="item">The item.</param>
        public static void AddAsUnique<T>(this List<T> list, T item)
        {
            if (!list.Contains(item))
            {
                list.Add(item);
            }
        }

        /// <summary>
        /// Check list and records. if list is not null and contains records, returns true, otherwise returns false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">List of T</param>
        /// <returns></returns>
        public static Boolean HasRecords<T>(this IEnumerable<T> list)
        {
            Boolean result = false;

            if (list != null)
            {
                result = list.Count() > 0;
            }

            return result;
        }

        /// <summary>
        /// To the data table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> items) where T : class
        {
            items.ThrowExceptionIfNull();
            var hasRecords = items.HasRecords();
            if (!hasRecords)
            {
                throw new Exception("Need at least one record in list!");
            }

            var dataTable = new DataTable(typeof(T).Name);

            var t = items.First();
            var properties = t.Properties();

            foreach (var prop in properties)
            {
                DataColumn column = new DataColumn(prop.Name, Type.GetType("System." + prop.ValueType));
                column.AllowDBNull = prop.Nullable;
                if (prop.ValueType == "String")
                    column.AllowDBNull = true;

                dataTable.Columns.Add(column);
            }

            foreach (var item in items)
            {
                var props = item.Properties();
                var values = props.Select(p => p.Value).ToArray();
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public static T PreviousItemIfExist<T>(this IList<T> list, T currentItem) where T : class
        {
            Int32 currentIndex = list.IndexOf(currentItem);
            Int32 previousIndex = currentIndex - 1;
            T previosItem = previousIndex >= 0 ? list[previousIndex] : null;
            return previosItem;
        }

        public static T NextItemIfExist<T>(this IList<T> list, T currentItem) where T : class
        {
            Int32 currentIndex = list.IndexOf(currentItem);
            Int32 nextIndex = currentIndex + 1;
            T previosItem = list.Count > nextIndex ? list[nextIndex] : null;
            return previosItem;
        }


        #endregion



    }
}
