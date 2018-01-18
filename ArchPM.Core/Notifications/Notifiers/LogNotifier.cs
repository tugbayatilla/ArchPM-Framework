using ArchPM.Core.Extensions;
using ArchPM.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArchPM.Core.Notifications.Notifiers
{
    public class LogNotifier : INotifierAsync
    {
        readonly LogToFileManager manager;

        public LogNotifier() : this(new IO.LogToFileManager())
        {
        }

        public LogNotifier(LogToFileManager manager)
        {
            manager.ThrowExceptionIfNull();
            this.manager = manager;
        }

        public async Task Notify(NotificationMessage notificationMessage)
        {
            notificationMessage.ThrowExceptionIfNull();
            await manager.AppendToFile(notificationMessage.Body);
        }

        public async Task Notify(string message)
        {
            await manager.AppendToFile(message);
        }

        public async Task Notify(Exception ex)
        {
            await manager.AppendToFile(ex.GetAllMessages(true," "));
        }


    }
}
