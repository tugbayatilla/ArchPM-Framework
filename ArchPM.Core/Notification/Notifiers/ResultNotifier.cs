using ArchPM.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArchPM.Core.Notification.Notifiers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Core.Notification.INotifier" />
    public class ConsoleResultNotifier : INotifier
    {


        /// <summary>
        /// Notifies the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Notify(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void Notify(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }

        /// <summary>
        /// Notifies the specified notification message.
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Notify(NotificationMessage notificationMessage)
        {
            notificationMessage.ThrowExceptionIfNull();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(notificationMessage.Body);
            Console.ResetColor();

        }
    }
}
