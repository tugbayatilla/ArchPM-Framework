using System;

namespace ArchPM.Core.Logging.BasicLogging
{
    /// <summary>
    /// Empty Basic Log 
    /// </summary>
    /// <seealso cref="ArchPM.Core.Logging.BasicLogging.IBasicLog" />
    public sealed class NullBasicLog : IBasicLog
    {
        /// <summary>
        /// Logs the specified ex.
        /// </summary>
        /// <typeparam name="T">Generic type of exception</typeparam>
        /// <param name="ex">The ex.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Log<T>(T ex) where T : Exception
        {
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Log(string message)
        {
        }

        
    }
}
