using System;

namespace ArchPM.Core.Logging.BasicLogging
{
    /// <summary>
    /// Basic Logging interface
    /// </summary>
    public interface IBasicLog
    {
        /// <summary>
        /// Logs the specified ex.
        /// </summary>
        /// <typeparam name="T">Generic type of exception</typeparam>
        /// <param name="ex">The ex.</param>
        void Log<T>(T ex) where T : Exception;


        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Log(String message);
    }
}
