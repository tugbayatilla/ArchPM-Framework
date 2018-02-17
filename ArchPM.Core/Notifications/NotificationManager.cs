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
    public class NotificationManager : INotification
    {
        /// <summary>
        /// The registered notifiers
        /// </summary>
        readonly Dictionary<String, List<INotifier>> registeredNotifiers;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationManager" /> class.
        /// </summary>
        public NotificationManager()
        {
            registeredNotifiers = new Dictionary<String, List<INotifier>>();
        }

        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="notifyTo">The notify to.  Use NotifyTo class</param>
        /// <returns></returns>
        public Task Notify(string message, params String[] notifyTo)
        {
            notifyTo.ThrowExceptionIfNull($"{nameof(notifyTo)} is null");

            registeredNotifiers.Keys.ToList().ForEach(p =>
            {
                if (notifyTo.Contains(p))
                {
                    registeredNotifiers[p].ForEach(x => x.Notify(message));
                }
            });

            return Task.FromResult(0);
        }

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="notifyTo">The notify to.  Use NotifyTo class</param>
        /// <returns></returns>
        public Task Notify(Exception ex, params String[] notifyTo)
        {
            notifyTo.ThrowExceptionIfNull($"{nameof(notifyTo)} is null");

            registeredNotifiers.Keys.ToList().ForEach(p =>
            {
                if (notifyTo.Contains(p))
                {
                    registeredNotifiers[p].ForEach(x => x.Notify(ex));
                }
            });
            return Task.FromResult(0);
        }

        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        /// <param name="notifyTo">The notify to. Use NotifyTo class</param>
        /// <returns></returns>
        public Task Notify(NotificationMessage notificationMessage, params String[] notifyTo)
        {
            notifyTo.ThrowExceptionIfNull($"{nameof(notifyTo)} is null");

            registeredNotifiers.Keys.ToList().ForEach(p =>
            {
                if (notifyTo.Contains(p))
                {
                    registeredNotifiers[p].ForEach(x => x.Notify(notificationMessage));
                }
            });
            return Task.FromResult(0);
        }

        /// <summary>
        /// Registers the notifier.
        /// </summary>
        /// <param name="notifyTo">The notify to. Use NotifyTo class</param>
        /// <param name="notifier">The notifier.</param>
        public void RegisterNotifier(String notifyTo, INotifier notifier)
        {
            notifyTo.ThrowExceptionIf(p => String.IsNullOrEmpty(notifyTo), $"{nameof(notifyTo)} is null.");
            notifier.ThrowExceptionIfNull($"{nameof(notifier)} is null.");


            if (!registeredNotifiers.ContainsKey(notifyTo))
            {
                registeredNotifiers.Add(notifyTo, new List<INotifier>());
            }

            registeredNotifiers[notifyTo].Add(notifier);
        }
    }
}
