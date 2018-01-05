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
    public enum NotificationLocations
    {
        /// <summary>
        /// The console
        /// </summary>
        Console = 1,
        /// <summary>
        /// The file
        /// </summary>
        File = 2,
        /// <summary>
        /// The event log
        /// </summary>
        EventLog = 4,
        /// <summary>
        /// The mail
        /// </summary>
        Mail = 8,
        /// <summary>
        /// The SMS
        /// </summary>
        SMS = 16,
        /// <summary>
        /// The result
        /// </summary>
        Result = 32
    }
}
