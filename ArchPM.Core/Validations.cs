using System;

namespace ArchPM.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class Validations
    {
        /// <summary>
        /// Throws the exception if.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="exception">The exception.</param>
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

        /// <summary>
        /// Throws the exception if null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="exception">The exception.</param>
        public static void ThrowExceptionIfNull<T>(this T obj, Exception exception = null)
        {
            obj.ThrowExceptionIf(p => p == null, exception);
        }

        /// <summary>
        /// Throws the exception if null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="message">The message.</param>
        public static void ThrowExceptionIfNull<T>(this T obj, String message)
        {
            obj.ThrowExceptionIf(p => p == null, new Exception(message));
        }

        /// <summary>
        /// Throws the exception if null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="message">The message.</param>
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
