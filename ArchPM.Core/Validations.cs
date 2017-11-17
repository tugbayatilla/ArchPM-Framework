using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Core
{
    public static class Validations
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="predicate"></param>
        /// <param name="exception"></param>
        public static void ThrowExceptionIf<T>(this T obj, Func<T, Boolean> predicate, Exception exception = null)
        {
            if (exception == null)
            {
                exception = new Exception(String.Format("Object is not meant to be!"));
            }
            if (predicate.Invoke(obj))
            {
                throw exception;
            }
        }

        public static void ThrowExceptionIfNull<T>(this T obj, Exception exception = null)
        {
            obj.ThrowExceptionIf(p => p == null, exception);
        }

        public static void ThrowExceptionIfNull<T>(this T obj, String message)
        {
            obj.ThrowExceptionIf(p => p == null, new Exception(message));
        }

        public static void ThrowExceptionIfNull<T>(this Object obj, String message = "") where T: Exception, new()
        {
            if(String.IsNullOrEmpty(message))
            {
                message = "A object instance can't be null";
            }
            var ex = (T)Activator.CreateInstance(typeof(T), message);

            ThrowExceptionIf(obj, p => p == null, ex);
        }
    }
}
