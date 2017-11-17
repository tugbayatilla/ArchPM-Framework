using System;
using System.Messaging;
using ArchPM.Core.Extensions;
using ArchPM.Messaging.MqLog.Infrastructure;

namespace ArchPM.Messaging.MqLog
{
    /// <summary>
    /// Msmq Custom Config
    /// </summary>
    /// <seealso cref="ArchPM.Messaging.MqConfig" />
    public class MqLogConfig : MqConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MqLogConfig" /> class.
        /// </summary>
        public MqLogConfig()
            : base()
        {
            this.QueueFormatter = new XmlMessageFormatter(new Type[] { typeof(MqLogMessageDTO), typeof(PropertyDTO) });
        }

    }
}
