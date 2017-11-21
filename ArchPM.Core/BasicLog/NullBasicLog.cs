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
        /// <param name="ex">The ex.</param>
        public void Log(Exception ex)
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
