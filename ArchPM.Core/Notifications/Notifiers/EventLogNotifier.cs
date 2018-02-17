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
        /// Notifies the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public Task Notify(string message)
        {
            // Write an informational entry to the event log.    
            var msg = String.Format("{0} {1}", DateTime.Now.ToMessageHeaderString(), message);
            myLog.WriteEntry(msg, EventLogEntryType.Information);

            return Task.FromResult(0);
        }

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public Task Notify(Exception ex)
        {
            // Write an informational entry to the event log.    
            var msg = String.Format("{0} {1}", DateTime.Now.ToMessageHeaderString(), ex.GetAllMessages());
            myLog.WriteEntry(msg, EventLogEntryType.Error);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Notifies the specified notification message.
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        public Task Notify(NotificationMessage notificationMessage)
        {
            notificationMessage.ThrowExceptionIfNull();
            var msg = String.Format("{0} Destination:{1} | Subject:{2} | Body:{3}", DateTime.Now.ToMessageHeaderString(), notificationMessage.Destination, notificationMessage.Subject, notificationMessage.Body);
            myLog.WriteEntry(msg, EventLogEntryType.Error);
            return Task.FromResult(0);
        }
    }
}
