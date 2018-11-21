using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery.AuthConfigSection.Model
{
    /// <summary>
    /// 
    /// </summary>
    public static class AuthConfigSectionExtensions
    {
        /// <summary>
        /// To the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this ConfigurationElementCollection collection)
        {
            List<T> result = new List<T>();
            var q = collection.GetEnumerator();

            while (q.MoveNext())
            {
                result.Add((T)q.Current);
            }
            return result;
        }
    }
}
