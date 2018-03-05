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
    /// uses LogFileTraceListener
    /// </summary>
    /// <seealso cref="ArchPM.Core.Notifications.INotifier" />
    public class LogTraceNotifier : INotifier
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
            this.Id = Guid.NewGuid();

            listener = new LogFileTraceListener();
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
        public Task Notify(NotificationMessage notificationMessage)
        {
            notificationMessage.ThrowExceptionIfNull();
            listener.WriteLine(notificationMessage.Body);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Notifies the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public Task Notify(string message)
        {
            listener.WriteLine(message);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public Task Notify(Exception ex)
        {
            listener.WriteLine(ex.GetAllMessages(true," "));
            return Task.FromResult(0);
        }

        /// <summary>
        /// Notifies the specified notification message.
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        /// <param name="notifyAs">The notify as.</param>
        /// <returns></returns>
        public Task Notify(NotificationMessage notificationMessage, NotifyAs notifyAs)
        {
            return Notify(notificationMessage, NotifyAs.Message);
        }

        /// <summary>
        /// Notifies the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="notifyAs">The notify as.</param>
        /// <returns></returns>
        public Task Notify(string message, NotifyAs notifyAs)
        {
            return Notify(message, NotifyAs.Message);
        }

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="notifyAs">The notify as.</param>
        /// <returns></returns>
        public Task Notify(Exception ex, NotifyAs notifyAs)
        {
            return Notify(ex, NotifyAs.Error);
        }

        /// <summary>
        /// Notifies the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="notifyAs">The notify as.</param>
        /// <returns></returns>
        public Task Notify(object entity, NotifyAs notifyAs)
        {
            var properties = entity.Properties().Where(p => p.IsPrimitive);
            StringBuilder sb = new StringBuilder();
            properties.ForEach(p => {
                sb.Append($"{p.Name}:{p.Value} | ");
            });

            var message = sb.ToString();
            return Notify(message, notifyAs);
        }
    }
}
