using System;
using System.Messaging;
using ArchPM.Core.Extensions;
using ArchPM.Messaging.MqLog.Infrastructure;

namespace ArchPM.Messaging.MqLog
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Messaging.MqServer" />
    public sealed class MqLogServer : MqServer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MqLogServer"/> class.
        /// </summary>
        /// <param name="configs">The configs.</param>
        public MqLogServer(params MqConfig[] configs)
            : base(configs)
        {
            foreach (var config in configs)
            {
                config.QueueFormatter = new XmlMessageFormatter(new Type[] { typeof(MqLogMessageDTO), typeof(PropertyDTO) });

            }
        }
    }

}
