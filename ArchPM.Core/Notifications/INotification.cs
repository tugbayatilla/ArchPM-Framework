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
    public interface INotification
    {
        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        /// <param name="notifyTo">The notify to.</param>
        /// <returns></returns>
        Task Notify(NotificationMessage notificationMessage, params String[] notifyTo);

        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        /// <param name="notifyAs">The notify as.</param>
        /// <param name="notifyTo">The notify to.</param>
        /// <returns></returns>
        Task Notify(NotificationMessage notificationMessage, NotifyAs notifyAs, params String[] notifyTo);


        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="notifyTo">The notify to.</param>
        /// <returns></returns>
        Task Notify(String message, params String[] notifyTo);

        /// <summary>
        /// Notify given message to given location or locations
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="notifyAs">The notify as.</param>
        /// <param name="notifyTo">The notify to.</param>
        /// <returns></returns>
        Task Notify(String message, NotifyAs notifyAs, params String[] notifyTo);


        /// <summary>
        /// Notifies the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="notifyTo">The notify to.</param>
        /// <returns></returns>
        Task Notify(String message, String notifyTo = NotifyTo.CONSOLE);

        /// <summary>
        /// Notifies the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="notifyAs">The notify as.</param>
        /// <param name="notifyTo">The notify to.</param>
        /// <returns></returns>
        Task Notify(String message, NotifyAs notifyAs, String notifyTo = NotifyTo.CONSOLE);



        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="notifyTo">The notify to.</param>
        /// <returns></returns>
        Task Notify(Exception ex, params String[] notifyTo);

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="notifyAs">The notify as.</param>
        /// <param name="notifyTo">The notify to.</param>
        /// <returns></returns>
        Task Notify(Exception ex, NotifyAs notifyAs, params String[] notifyTo);


        /// <summary>
        /// Registers the notifier.
        /// </summary>
        /// <param name="notifyTo">The notify to.</param>
        /// <param name="notifier">The notifier.</param>
        void RegisterNotifier(String notifyTo, INotifier notifier);

        /// <summary>
        /// Unregisters the notifier.
        /// </summary>
        /// <param name="notifyTo">The notify to.</param>
        /// <param name="notifierId">The notifier identifier.</param>
        void UnregisterNotifier(String notifyTo, Guid notifierId);

    }

    
}
