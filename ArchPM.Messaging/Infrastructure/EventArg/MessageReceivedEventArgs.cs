using System;

namespace ArchPM.Messaging.Infrastructure.EventArg
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class MessageReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the message object.
        /// </summary>
        /// <value>
        /// The message object.
        /// </value>
        public MessageObject MessageObject { get; set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public MqConfig Config { get; set; }
    }
}
