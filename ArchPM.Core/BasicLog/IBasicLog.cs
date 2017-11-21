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
        /// <param name="ex">The ex.</param>
        void Log(Exception ex);


        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Log(String message);
    }
}
