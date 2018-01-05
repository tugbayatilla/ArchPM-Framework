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
    public class NotificationManager : INotificationManager
    {
        /// <summary>
        /// The registered notifiers
        /// </summary>
        readonly Dictionary<NotificationLocations, List<INotifier>> registeredNotifiers;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationManager"/> class.
        /// </summary>
        public NotificationManager()
        {
            registeredNotifiers = new Dictionary<NotificationLocations, List<INotifier>>();
        }

        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="notificationLocation">Default: Console | File</param>
        public void Notify(string message, NotificationLocations notificationLocation)
        {
            registeredNotifiers.Keys.ToList().ForEach(p =>
            {
                if (notificationLocation.HasFlag(p))
                {
                    registeredNotifiers[p].ForEach(x => x.Notify(message));
                }
            });
        }

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="notificationLocation">The notification location.</param>
        public void Notify(Exception ex, NotificationLocations notificationLocation = NotificationLocations.EventLog)
        {
            registeredNotifiers.Keys.ToList().ForEach(p =>
            {
                if (notificationLocation.HasFlag(p))
                {
                    registeredNotifiers[p].ForEach(x => x.Notify(ex));
                }
            });
        }

        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        /// <param name="notificationLocation">The notification location.</param>
        public void Notify(NotificationMessage notificationMessage, NotificationLocations notificationLocation)
        {
            registeredNotifiers.Keys.ToList().ForEach(p =>
            {
                if (notificationLocation.HasFlag(p))
                {
                    registeredNotifiers[p].ForEach(x => x.Notify(notificationMessage));
                }
            });
        }

        /// <summary>
        /// Registers the notifier.
        /// </summary>
        /// <param name="notificationLocation">The notification location.</param>
        /// <param name="notifier">The notifier.</param>
        public void RegisterNotifier(NotificationLocations notificationLocation, INotifier notifier)
        {
            if (!registeredNotifiers.ContainsKey(notificationLocation))
            {
                registeredNotifiers.Add(notificationLocation, new List<INotifier>());
            }

            registeredNotifiers[notificationLocation].Add(notifier);
        }
    }
}
