using ArchPM.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class EventLogNotifier : INotifier
    {
        readonly EventLog myLog;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventLogNotifier"/> class.
        /// </summary>
        public EventLogNotifier()
        {
            myLog = new EventLog("Application")
            {
                Source = "NotImplemented"
            };

            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventLogNotifier"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public EventLogNotifier(String source) : this()
        {
            myLog.Source = source;
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

            var eventLogEntryType = ConvertNotifyAsToEventLogEntryType(notifyAs);

            myLog.WriteEntry(msg, eventLogEntryType);
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
            var eventLogEntryType = ConvertNotifyAsToEventLogEntryType(notifyAs);

            // Write an informational entry to the event log.    
            var msg = $"{DateTime.Now.ToMessageHeaderString()}[{notifyAs.GetName()}] {message}";
            myLog.WriteEntry(msg, eventLogEntryType);
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
            var eventLogEntryType = ConvertNotifyAsToEventLogEntryType(notifyAs);

            // Write an informational entry to the event log.    
            var msg = $"{DateTime.Now.ToMessageHeaderString()}[{notifyAs.GetName()}] { ex.GetAllMessages()}";
            myLog.WriteEntry(msg, eventLogEntryType);
            return Task.FromResult(0);
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

        /// <summary>
        /// Converts the type of the notify as to event log entry.
        /// </summary>
        /// <param name="notifyAs">The notify as.</param>
        /// <returns></returns>
        EventLogEntryType ConvertNotifyAsToEventLogEntryType(NotifyAs notifyAs)
        {
            var eventLogEntryType = EventLogEntryType.Error;
            switch (notifyAs)
            {
                case NotifyAs.Message:
                    eventLogEntryType = EventLogEntryType.Information;
                    break;
                case NotifyAs.Warning:
                    eventLogEntryType = EventLogEntryType.Warning;
                    break;
                case NotifyAs.Error:
                default:
                    break;
            }

            return eventLogEntryType;
        }

    }
}
