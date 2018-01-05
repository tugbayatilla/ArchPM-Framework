using ArchPM.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArchPM.Core.Notifications.Notifiers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Core.Notifications.INotifierAsync" />
    public class ConsoleNotifier : INotifierAsync
    {
        /// <summary>
        /// Notifies the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public Task Notify(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            var msg = String.Format("[{0:dd-MM-yyyy HH:mm:ss.fffff}][1] {2}", DateTime.Now, Thread.CurrentThread.ManagedThreadId, message);
            Console.WriteLine(msg);
            Console.ResetColor();

            return Task.FromResult(0);
        }

        /// <summary>
        /// Notifies the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public Task Notify(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            var msg = String.Format("[{0:dd-MM-yyyy HH:mm:ss.fffff}][1] {2}", DateTime.Now, Thread.CurrentThread.ManagedThreadId, ex.GetAllMessages());
            Console.WriteLine(msg);
            Console.ResetColor();
            return Task.FromResult(0);
        }

        /// <summary>
        /// Notifies the specified notification message.
        /// </summary>
        /// <param name="notificationMessage">The notification message.</param>
        public Task Notify(NotificationMessage notificationMessage)
        {
            notificationMessage.ThrowExceptionIfNull();

            Console.ForegroundColor = ConsoleColor.Green;
            var msg = String.Format("[{0:dd-MM-yyyy HH:mm:ss.fffff}][1] Destination:{2} | Subject:{3} | Body:{4}", DateTime.Now, Thread.CurrentThread.ManagedThreadId, notificationMessage.Destination, notificationMessage.Subject, notificationMessage.Body);
            Console.WriteLine(msg);
            Console.ResetColor();

            return Task.FromResult(0);
        }

    }
}
