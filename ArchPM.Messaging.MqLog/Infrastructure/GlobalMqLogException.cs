using System;

namespace ArchPM.Messaging.MqLog.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class GlobalMqLogException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalMqLogException"/> class.
        /// </summary>
        public GlobalMqLogException()
        {
            this.CreateTime = DateTime.Now;
        }
        /// <summary>
        /// Gets or sets the name of the entity.
        /// </summary>
        /// <value>
        /// The name of the entity.
        /// </value>
        public String EntityName { get; set; }
        /// <summary>
        /// Gets the create time.
        /// </summary>
        /// <value>
        /// The create time.
        /// </value>
        public DateTime CreateTime { get; private set; }
        /// <summary>
        /// Gets or sets the message dto string.
        /// </summary>
        /// <value>
        /// The message dto string.
        /// </value>
        public String MessageDtoString { get; set; }
        /// <summary>
        /// Gets or sets the exception message string.
        /// </summary>
        /// <value>
        /// The exception message string.
        /// </value>
        public String ExceptionMessageString { get; set; }
    }
}
