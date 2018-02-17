using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Core.Notifications
{
    /// <summary>
    /// Notification and Log 
    /// </summary>
    [Obsolete("Use INotification. this will be removed later versions.")]
    public interface INotificationAsync
    {
        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        /// <param name="notificationLocation">The notification location.</param>
        [Obsolete("Use INotification.NotifyAsync with NotificationLocation as String version method. this will be removed later versions.")]
        Task NotifyAsync(NotificationMessage notificationMessage, NotificationLocations notificationLocation);


        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="notificationLocation">Default: Console | File</param>
        [Obsolete("Use INotification.NotifyAsync with NotificationLocation as String version method. this will be removed later versions")]
        Task NotifyAsync(String message, NotificationLocations notificationLocation = NotificationLocations.Console | NotificationLocations.File);

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="notificationLocation">The notification location.</param>
        Task NotifyAsync(Exception ex, NotificationLocations notificationLocation = NotificationLocations.EventLog);

        /// <summary>
        /// Registers the notifier.
        /// </summary>
        /// <param name="notificationLocation">The notification location.</param>
        /// <param name="notifier">The notifier.</param>
        void RegisterNotifier(NotificationLocations notificationLocation, INotifierAsync notifier);
    }


}
