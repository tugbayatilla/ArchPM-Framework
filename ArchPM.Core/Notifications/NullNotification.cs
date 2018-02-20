using System;
using System.Threading.Tasks;

namespace ArchPM.Core.Notifications
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Core.Notifications.INotification" />
    public class NullNotification : INotification
    {
        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        /// <param name="notifyTo">The notify to. Use NotifyTo class</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task Notify(NotificationMessage notificationMessage, params string[] notifyTo)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="notifyTo">The notify to. Use NotifyTo class</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task Notify(string message, params string[] notifyTo)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="notifyTo">The notify to.  Use NotifyTo class</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task Notify(Exception ex, params string[] notifyTo)
        {
            return Task.FromResult(0);
        }

        public Task Notify(string message, string notifyTo = "Console")
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Registers the notifier.
        /// </summary>
        /// <param name="notifyTo">The notify to.  Use NotifyTo class</param>
        /// <param name="notifier">The notifier.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void RegisterNotifier(string notifyTo, INotifier notifier)
        {
            //nothing
        }
    }
}
