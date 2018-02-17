using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Core.Notifications
{
    /// <summary>
    /// 
    /// </summary>
    [Obsolete("Use INotificationManager. this will be removed later versions.")]
    public interface INotifierAsync
    {
        /// <summary>
        /// Notifies the specified notification message.
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        Task Notify(NotificationMessage notificationMessage);
        /// <summary>
        /// Notifies the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        Task Notify(String message);
        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        Task Notify(Exception ex);
    }
}
