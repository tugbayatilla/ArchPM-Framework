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
        static Object _lock = new Object();

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
            return Notify(message, NotifyAs.Message, notifyTo);
        }

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="notifyTo">The notify to.  Use NotifyTo class</param>
        /// <returns></returns>
        public Task Notify(Exception ex, params String[] notifyTo)
        {
            return Notify(ex, NotifyAs.Error, notifyTo);
        }

        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        /// <param name="notifyTo">The notify to. Use NotifyTo class</param>
        /// <returns></returns>
        public Task Notify(NotificationMessage notificationMessage, params String[] notifyTo)
        {
            return Notify(notificationMessage, NotifyAs.Message, notifyTo);
        }

        /// <summary>
        /// Notifies the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="notifyTo">The notify to.</param>
        /// <returns></returns>
        public Task Notify(string message, string notifyTo = NotifyTo.CONSOLE)
        {
            return Notify(message, NotifyAs.Message, new String[] { notifyTo });
        }

        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        /// <param name="notifyAs">The notify as.</param>
        /// <param name="notifyTo">The notify to.</param>
        /// <returns></returns>
        public Task Notify(NotificationMessage notificationMessage, NotifyAs notifyAs, params string[] notifyTo)
        {
            notifyTo.ThrowExceptionIfNull($"{nameof(notifyTo)} is null");

            registeredNotifiers.Keys.ToList().ForEach(p =>
            {
                if (notifyTo.Contains(p))
                {
                    registeredNotifiers[p].ForEach(x => x.Notify(notificationMessage, notifyAs));
                }
            });

            return Task.FromResult(0);
        }

        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="notifyAs">The notify as.</param>
        /// <param name="notifyTo">The notify to.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task Notify(string message, NotifyAs notifyAs, params string[] notifyTo)
        {
            notifyTo.ThrowExceptionIfNull($"{nameof(notifyTo)} is null");

            registeredNotifiers.Keys.ToList().ForEach(p =>
            {
                if (notifyTo.Contains(p))
                {
                    registeredNotifiers[p].ForEach(x => x.Notify(message, notifyAs));
                }
            });

            return Task.FromResult(0);
        }

        /// <summary>
        /// Notifies the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="notifyAs">The notify as.</param>
        /// <param name="notifyTo">The notify to.</param>
        /// <returns></returns>
        public Task Notify(string message, NotifyAs notifyAs, string notifyTo = NotifyTo.CONSOLE)
        {
            return Notify(message, notifyAs, new String[] { notifyTo });
        }

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="notifyAs">The notify as.</param>
        /// <param name="notifyTo">The notify to.</param>
        /// <returns></returns>
        public Task Notify(Exception ex, NotifyAs notifyAs, params string[] notifyTo)
        {
            notifyTo.ThrowExceptionIfNull($"{nameof(notifyTo)} is null");

            registeredNotifiers.Keys.ToList().ForEach(p =>
            {
                if (notifyTo.Contains(p))
                {
                    registeredNotifiers[p].ForEach(x => x.Notify(ex, notifyAs));
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

            lock (_lock)
            {
                if (!registeredNotifiers.ContainsKey(notifyTo))
                {
                    registeredNotifiers.Add(notifyTo, new List<INotifier>());
                }

                registeredNotifiers[notifyTo].Add(notifier);
            }
        }

        /// <summary>
        /// Unregisters the notifier.
        /// </summary>
        /// <param name="notifyTo">The notify to.</param>
        /// <param name="notifierId">The notifier identifier.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void UnregisterNotifier(string notifyTo, Guid notifierId)
        {
            lock (_lock)
            {
                if (registeredNotifiers.ContainsKey(notifyTo))
                {
                    var notifiers = registeredNotifiers.First(p => p.Key == notifyTo);
                    var notifier = notifiers.Value.FirstOrDefault(p => p.Id == notifierId);
                    if (notifier != null)
                    {
                        notifiers.Value.Remove(notifier);
                    }
                }
            }
        }

        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="notifyAs">The notify as.</param>
        /// <param name="notifyTo">The notify to.</param>
        /// <returns></returns>
        public Task Notify(object entity, NotifyAs notifyAs, params string[] notifyTo)
        {
            notifyTo.ThrowExceptionIfNull($"{nameof(notifyTo)} is null");

            registeredNotifiers.Keys.ToList().ForEach(p =>
            {
                if (notifyTo.Contains(p))
                {
                    registeredNotifiers[p].ForEach(x => x.Notify(entity, notifyAs));
                }
            });
            return Task.FromResult(0);
        }
    }
}
