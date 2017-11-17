using System;


namespace ArchPM.Messaging.MqLog.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class MqLogDbConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MqLogDbConfig"/> class.
        /// </summary>
        public MqLogDbConfig()
        {
            this.TableNamePrefix = "ArchPMLOG";
        }
        /// <summary>
        /// Gets or sets the table name prefix.
        /// </summary>
        /// <value>
        /// The table name prefix.
        /// </value>
        public String TableNamePrefix { get; set; }
        /// <summary>
        /// Gets or sets the table name suffix.
        /// </summary>
        /// <value>
        /// The table name suffix.
        /// </value>
        public String TableNameSuffix { get; set; }
    }

}
