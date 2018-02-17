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
    [Obsolete("Use NotifyTo. this will be removed later versions.")]
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
        Result = 32,
        /// <summary>
        /// The Log
        /// </summary>
        Log = 64,
        /// <summary>
        /// The Log2
        /// </summary>
        Log2 = 128,
        /// <summary>
        /// The Log3
        /// </summary>
        Log3 = 256
    }



}
