using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using ArchPM.Core.Extensions;

namespace ArchPM.Messaging.MqLog.Infrastructure
{
    /// <summary>
    /// Msmq Message Data Transfere Object is using to transfer objects from app to msmq and from msmq to custom msmqserver
    /// </summary>
    [XmlRoot("MqLogMessageDTO")]
    [Serializable]
    public class MqLogMessageDTO
    {
        /// <summary>
        /// Gets or sets the tby message identifier.
        /// </summary>
        /// <value>
        /// The tby message identifier.
        /// </value>
        public String TBYMessageID { get; set; }
        /// <summary>
        /// Gets or sets the type of the tby message.
        /// </summary>
        /// <value>
        /// The type of the tby message.
        /// </value>
        public MqLogMessageTypes TBYMessageType { get; set; }
        /// <summary>
        /// Gets or sets the tby message create time.
        /// </summary>
        /// <value>
        /// The tby message create time.
        /// </value>
        public DateTime TBYMessageCreateTime { get; set; }
        /// <summary>
        /// Gets or sets the entity properties.
        /// </summary>
        /// <value>
        /// The entity properties.
        /// </value>
        public List<PropertyDTO> Properties { get; set; }
        /// <summary>
        /// Gets or sets the name of the tby entity.
        /// </summary>
        /// <value>
        /// The name of the tby entity.
        /// </value>
        public String TBYEntityName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MqLogMessageDTO"/> class.
        /// </summary>
        public MqLogMessageDTO()
        {
            this.TBYMessageID = "";
            this.TBYMessageCreateTime = DateTime.Now;
            this.Properties = new List<PropertyDTO>();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("[TBYMessageID:{0}]", this.TBYMessageID);
            sb.AppendFormat("[TBYMessageType:{0}]", this.TBYMessageType);
            sb.AppendFormat("[TBYMessageCreateTime:{0}]", this.TBYMessageCreateTime);
            sb.AppendFormat("[TBYEntityName:{0}]", this.TBYEntityName);
            foreach (var item in this.Properties)
            {
                sb.AppendFormat("[{0}:{1}]", item.Name, item.Value);
            }

            return sb.ToString();
        }
    }

}

