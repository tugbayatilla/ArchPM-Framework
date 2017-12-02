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
    public abstract class NotificationMessage
    {
        /// <summary>
        /// Gets and Sets Destination, i.e. To email, or SMS phone number
        /// </summary>
        /// <value>
        /// The destination.
        /// </value>
        public virtual string Destination { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>

        public virtual string Subject { get; set; }
        
        /// <summary>
        /// Gets or sets Message contents
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public virtual string Body { get; set; }
    }
}
