using ArchPM.Core.Extensions;
using ArchPM.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArchPM.Core.Notifications.Notifiers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Core.Notifications.INotifierAsync" />
    public class LogTraceNotifier : INotifierAsync
    {
        readonly LogToFileManager manager;
        LogFileTraceListener listener;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogNotifier"/> class.
        /// </summary>
        public LogTraceNotifier() : this(new IO.LogToFileManager())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogNotifier"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public LogTraceNotifier(LogToFileManager manager)
        {
            manager.ThrowExceptionIfNull();
            this.manager = manager;

            listener = new LogFileTraceListener();
        }

        /// <summary>
        /// Notifies the specified notification message.
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        /// <returns></returns>
        public async Task Notify(NotificationMessage notificationMessage)
        {
            notificationMessage.ThrowExceptionIfNull();
            listener.WriteLine(notificationMessage.Body);
        }

        /// <summary>
        /// Notifies the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task Notify(string message)
        {
            listener.WriteLine(message);
        }

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public async Task Notify(Exception ex)
        {
            listener.WriteLine(ex.GetAllMessages(true," "));
        }


    }
}
