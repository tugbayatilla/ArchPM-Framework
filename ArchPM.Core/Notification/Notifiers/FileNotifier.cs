using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ArchPM.Core.Extensions;

namespace ArchPM.Core.Notification.Notifiers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Core.Notification.INotifier" />
    public class FileNotifier : INotifier
    {
        /// <summary>
        /// Notifies the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Notify(string message)
        {
            var msg = String.Format("[{0:dd-MM-yyyy HH:mm:ss.fffff}][1] {2}", DateTime.Now, Thread.CurrentThread.ManagedThreadId, message);
            Trace.WriteLine(msg);
        }

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void Notify(Exception ex)
        {
            Notify(ex.GetAllMessages());
        }

        /// <summary>
        /// Notifies the specified notification message.
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        public void Notify(NotificationMessage notificationMessage)
        {
            notificationMessage.ThrowExceptionIfNull();
            var msg = String.Format("[{0:dd-MM-yyyy HH:mm:ss.fffff}][1] Destination:{2} | Subject:{3} | Body:{4}", DateTime.Now, Thread.CurrentThread.ManagedThreadId, notificationMessage.Destination, notificationMessage.Subject, notificationMessage.Body);
            Trace.WriteLine(msg);
        }
    }
}
