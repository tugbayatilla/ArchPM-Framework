using ArchPM.Core.Exceptions;
using System;

namespace ArchPM.Core.Notifications
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Exception" />
    /// <seealso cref="ArchPM.Core.Exceptions.IArchPMException" />
    public class NotificationException : Exception, IArchPMException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NotificationException(String message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public NotificationException(String message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException" /> class.
        /// </summary>
        public NotificationException()
        {

        }
    }
}
