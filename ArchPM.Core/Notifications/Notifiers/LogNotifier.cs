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
    /// <seealso cref="ArchPM.Core.Notifications.INotifier" />
    public class LogNotifier : INotifier
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
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; private set; }

        /// <summary>
        /// Notifies the specified notification message.
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        /// <returns></returns>
        public async Task Notify(NotificationMessage notificationMessage)
        {
            await Notify(notificationMessage, NotifyAs.Message);
        }

        /// <summary>
        /// Notifies the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task Notify(string message)
        {
            await Notify(message, NotifyAs.Message);
        }

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public async Task Notify(Exception ex)
        {
            await Notify(ex, NotifyAs.Error);
        }

        /// <summary>
        /// Notifies the specified notification message.
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        /// <param name="notifyAs">The notify as.</param>
        /// <returns></returns>
        public async Task Notify(NotificationMessage notificationMessage, NotifyAs notifyAs)
        {
            notificationMessage.ThrowExceptionIfNull();

            await manager.AppendToFile(notificationMessage.Body, notifyAs);
        }

        /// <summary>
        /// Notifies the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="notifyAs">The notify as.</param>
        /// <returns></returns>
        public async Task Notify(string message, NotifyAs notifyAs)
        {
            await manager.AppendToFile(message, notifyAs);
        }

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="notifyAs">The notify as.</param>
        /// <returns></returns>
        public async Task Notify(Exception ex, NotifyAs notifyAs)
        {
            await manager.AppendToFile(ex.GetAllMessages(true, " "), notifyAs);
        }
    }
}
