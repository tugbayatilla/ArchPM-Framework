using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchPM.Core.Notifications
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Core.Notifications.INotification" />
    [Obsolete("Use INotificationManager. this will be removed later versions.")]
    public class NotificationAsyncManager : INotificationAsync
    {
        /// <summary>
        /// The registered notifiers
        /// </summary>
        readonly Dictionary<NotificationLocations, List<INotifierAsync>> registeredNotifiers;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationAsyncManager"/> class.
        /// </summary>
        public NotificationAsyncManager()
        {
            registeredNotifiers = new Dictionary<NotificationLocations, List<INotifierAsync>>();
        }

        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="notificationLocation">Default: Console | File</param>
        /// <returns></returns>
        public Task NotifyAsync(string message, NotificationLocations notificationLocation)
        {
            registeredNotifiers.Keys.ToList().ForEach(p =>
            {
                if (notificationLocation.HasFlag(p))
                {
                    registeredNotifiers[p].ForEach(async x => await x.Notify(message));
                }
            });

            return Task.FromResult(0);
        }

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="notificationLocation">The notification location.</param>
        public Task NotifyAsync(Exception ex, NotificationLocations notificationLocation = NotificationLocations.EventLog)
        {
            registeredNotifiers.Keys.ToList().ForEach(p =>
            {
                if (notificationLocation.HasFlag(p))
                {
                    registeredNotifiers[p].ForEach(async x => await x.Notify(ex));
                }
            });
            return Task.FromResult(0);
        }

        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        /// <param name="notificationLocation">The notification location.</param>
        public Task NotifyAsync(NotificationMessage notificationMessage, NotificationLocations notificationLocation)
        {
            registeredNotifiers.Keys.ToList().ForEach(p =>
            {
                if (notificationLocation.HasFlag(p))
                {
                    registeredNotifiers[p].ForEach(async x => await x.Notify(notificationMessage));
                }
            });
            return Task.FromResult(0);
        }

        /// <summary>
        /// Registers the notifier.
        /// </summary>
        /// <param name="notificationLocation">The notification location.</param>
        /// <param name="notifier">The notifier.</param>
        public void RegisterNotifier(NotificationLocations notificationLocation, INotifierAsync notifier)
        {
            if (!registeredNotifiers.ContainsKey(notificationLocation))
            {
                registeredNotifiers.Add(notificationLocation, new List<INotifierAsync>());
            }

            registeredNotifiers[notificationLocation].Add(notifier);
        }
    }
}
