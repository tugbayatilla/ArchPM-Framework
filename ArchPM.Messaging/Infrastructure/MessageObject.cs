using System;
using System.Messaging;
using ArchPM.Core;

namespace ArchPM.Messaging.Infrastructure
{
    /// <summary>
    /// Holds the Message Information
    /// </summary>
    public class MessageObject
    {
        Message message;

        /// <summary>
        /// Gets and Sets the Transaction
        /// </summary>
        /// <value>
        /// The transaction.
        /// </value>
        public MessageQueueTransaction Transaction { get; set; }

        /// <summary>
        /// Gets and Sets the Message
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public Message Message 
        {
            get { return message; }
            set
            {
                message = value;
                message.ThrowExceptionIfNull("MessageObject.Message is null!!!"); //fix: 02.05.2016 object ref hatasina istinaden

                this.MessageBody = message.Body;
                this.MessageAppSpecific = message.AppSpecific;
            }
        }

        /// <summary>
        /// Gets and Sets the Message send or received in trasaction
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is transactional; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsTransactional { get; set; }

        /// <summary>
        /// Gets the Message Body
        /// </summary>
        /// <value>
        /// The message body.
        /// </value>
        public Object MessageBody { get; private set; }

        /// <summary>
        /// Gets the Message AppSpecific
        /// </summary>
        /// <value>
        /// The message application specific.
        /// </value>
        public Int32 MessageAppSpecific { get; private set; }
    }
}
