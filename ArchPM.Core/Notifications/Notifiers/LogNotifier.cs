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
    public class LogNotifier : INotifierAsync
    {
        readonly LogToFileManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogNotifier"/> class.
        /// </summary>
        public LogNotifier() : this(new IO.LogToFileManager())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogNotifier"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public LogNotifier(LogToFileManager manager)
        {
            manager.ThrowExceptionIfNull();
            this.manager = manager;
        }

        /// <summary>
        /// Notifies the specified notification message.
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        /// <returns></returns>
        public async Task Notify(NotificationMessage notificationMessage)
        {
            notificationMessage.ThrowExceptionIfNull();
            await manager.AppendToFile(notificationMessage.Body);
        }

        /// <summary>
        /// Notifies the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task Notify(string message)
        {
            await manager.AppendToFile(message);
        }

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public async Task Notify(Exception ex)
        {
            await manager.AppendToFile(ex.GetAllMessages(true," "));
        }


    }
}
