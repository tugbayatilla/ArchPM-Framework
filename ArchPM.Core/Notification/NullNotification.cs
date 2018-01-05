using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Core.Notification
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Core.Notification.INotificationManager" />
    public class NullNotification : INotificationManager
    {
        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="notificationLocation">Default: Console | File</param>
        public void Notify(string message, NotificationLocations notificationLocation)
        {
            //do nothing here
        }

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="notificationLocation">The notification location.</param>
        public void Notify(Exception ex, NotificationLocations notificationLocation = NotificationLocations.EventLog)
        {
            //do nothing here
        }

        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        /// <param name="notificationLocation">The notification location.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Notify(NotificationMessage notificationMessage, NotificationLocations notificationLocation)
        {
            // do nothing here
        }

        /// <summary>
        /// Registers the notifier.
        /// </summary>
        /// <param name="notificationLocation">The notification location.</param>
        /// <param name="notifier">The notifier.</param>
        public void RegisterNotifier(NotificationLocations notificationLocation, INotifier notifier)
        {
            //do nothing here
        }
    }
}
