using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ArchPM.Core.Extensions;

namespace ArchPM.Core.Notifications.Notifiers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Core.Notifications.INotifier" />
    public class FileNotifier : INotifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileNotifier"/> class.
        /// </summary>
        public FileNotifier()
        {
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
        /// Notifies the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public Task Notify(string message)
        {
            return Notify(message, NotifyAs.Message);
        }

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public Task Notify(Exception ex)
        {
            return Notify(ex, NotifyAs.Error);
        }

        /// <summary>
        /// Notifies the specified notification message.
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        public Task Notify(NotificationMessage notificationMessage)
        {
            return Notify(notificationMessage, NotifyAs.Message);
        }

        /// <summary>
        /// Notifies the specified notification message.
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        /// <param name="notifyAs">The notify as.</param>
        /// <returns></returns>
        public Task Notify(NotificationMessage notificationMessage, NotifyAs notifyAs)
        {
            notificationMessage.ThrowExceptionIfNull();
            var msg = String.Format("{0}[{4}] Destination:{1} | Subject:{2} | Body:{3}", 
                DateTime.Now.ToMessageHeaderString(), 
                notificationMessage.Destination, 
                notificationMessage.Subject, 
                notificationMessage.Body,
                notifyAs.GetName());

            Trace.WriteLine(msg);

            return Task.FromResult(0);
        }

        /// <summary>
        /// Notifies the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="notifyAs">The notify as.</param>
        /// <returns></returns>
        public Task Notify(string message, NotifyAs notifyAs)
        {
            var msg = $"{DateTime.Now.ToMessageHeaderString()}[{notifyAs.GetName()}] {message}";
            Trace.WriteLine(msg);

            return Task.FromResult(0);
        }

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="notifyAs">The notify as.</param>
        /// <returns></returns>
        public Task Notify(Exception ex, NotifyAs notifyAs)
        {
            return Notify(ex.GetAllMessages(), notifyAs);
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
