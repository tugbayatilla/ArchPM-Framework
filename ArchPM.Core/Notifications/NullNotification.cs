using System;
using System.Threading.Tasks;

namespace ArchPM.Core.Notifications
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Core.Notifications.INotificationAsync" />
    public class NullNotification : INotificationAsync
    {
        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="notificationLocation">Default: Console | File</param>
        public Task NotifyAsync(string message, NotificationLocations notificationLocation)
        {
            //do nothing here
            return Task.FromResult(0);
        }

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="notificationLocation">The notification location.</param>
        public Task NotifyAsync(Exception ex, NotificationLocations notificationLocation = NotificationLocations.EventLog)
        {
            //do nothing here
            return Task.FromResult(0);
        }

        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        /// <param name="notificationLocation">The notification location.</param>
        /// <exception cref="NotImplementedException"></exception>
        public Task NotifyAsync(NotificationMessage notificationMessage, NotificationLocations notificationLocation)
        {
            // do nothing here
            return Task.FromResult(0);
        }

        /// <summary>
        /// Registers the notifier.
        /// </summary>
        /// <param name="notificationLocation">The notification location.</param>
        /// <param name="notifier">The notifier.</param>
        public void RegisterNotifier(NotificationLocations notificationLocation, INotifierAsync notifier)
        {
            //do nothing here
        }

       
    }
}
