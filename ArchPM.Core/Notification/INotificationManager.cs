using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Core.Notification
{
    /// <summary>
    /// Notification and Log 
    /// </summary>
    public interface INotificationManager
    {
        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        /// <param name="notificationLocation">The notification location.</param>
        void Notify(NotificationMessage notificationMessage, NotificationLocations notificationLocation);


        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="notificationLocation">Default: Console | File</param>
        void Notify(String message, NotificationLocations notificationLocation = NotificationLocations.Console | NotificationLocations.File);

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="notificationLocation">The notification location.</param>
        void Notify(Exception ex, NotificationLocations notificationLocation = NotificationLocations.EventLog);

        /// <summary>
        /// Registers the notifier.
        /// </summary>
        /// <param name="notificationLocation">The notification location.</param>
        /// <param name="notifier">The notifier.</param>
        void RegisterNotifier(NotificationLocations notificationLocation, INotifier notifier);
    }

    
}
