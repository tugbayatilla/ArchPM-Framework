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
    public interface INotification
    {
        /// <summary>
        /// Notifies the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Notify(NotificationMessage message);
    }
}
