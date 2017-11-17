using System;
using System.Messaging;
using ArchPM.Core;
using ArchPM.Messaging.Infrastructure;
using ArchPM.Messaging.Infrastructure.EventArg;

namespace ArchPM.Messaging
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MqReader
    {

        /// <summary>
        /// Occurs when [on error while receiving].
        /// </summary>
        public event EventHandler<ErrorWhileReceivingEventArgs> OnErrorWhileReceiving = delegate { };
        /// <summary>
        /// Occurs when [on message received].
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs> OnMessageReceived = delegate { };
        /// <summary>
        /// Occurs when [on reader stopped].
        /// </summary>
        public event EventHandler<MqConfig> OnReaderStopped = delegate { };

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public MqConfig Config { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="MqReader"/> is reading.
        /// </summary>
        /// <value>
        ///   <c>true</c> if reading; otherwise, <c>false</c>.
        /// </value>
        public Boolean Reading { get; private set; }

        public IBasicLog BasicLog { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MqReader"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public MqReader(MqConfig config)
        {
            this.BasicLog = new NullBasicLog();

            this.Config = config;
        }


        /// <summary>
        /// Starts the reading.
        /// </summary>
        public void StartReading()
        {
            if (this.Reading)
            {
                this.BasicLog.Log(String.Format("[MqReader] Reader is Already Reading! Queue '{0}' at '{1}'!",
                    Config.QueueName
                   , Config.GetServerName()));

                return;
            }
            else
            {
                this.BasicLog.Log(String.Format("[MqReader] Reader is Started Reading! Queue '{0}' at '{1}'!",
                    Config.QueueName
                   , Config.GetServerName()));

                this.BasicLog.Log(String.Format("[MqReader] Queue ActivePath: '{0}'", Config.ActivePath));

                this.Reading = true;
            }

            while (true)
            {
                try
                {
                    var activePath = Config.GetActivePath();

                    using (MessageQueue queue = new MessageQueue(activePath))
                    {
                        queue.MessageReadPropertyFilter.SetAll();//todo: get only required properties of message such as appspecific
                        queue.Formatter = this.Config.QueueFormatter;

                        MessageReceivedEventArgs args = new MessageReceivedEventArgs();
                        args.Config = Config;

                        if (!Config.IsRemoteQueue() && !MessageQueue.Exists(activePath))
                        {
                            this.BasicLog.Log(String.Format("[MqReader] Queue is NOT Exist! Queue '{0}' at '{1}'!",
                                Config.QueueName
                              , Config.GetServerName()));

                            break; //stoping the reader
                        }

                        Message message = null;
                        if (queue.CanRead)
                        {
                            if (Config.IsRemoteQueue())
                            {
                                message = getMessage(queue);
                                args.MessageObject = new MessageObject() { IsTransactional = false, Message = message };
                            }
                            else
                            {
                                if (queue.Transactional)
                                {
                                    var transaction = new MessageQueueTransaction();
                                    transaction.Begin();
                                    message = getMessage(queue, transaction);
                                    args.MessageObject = new MessageObject() { IsTransactional = true, Message = message, Transaction = transaction };
                                }
                                else
                                {
                                    message = getMessage(queue);
                                    args.MessageObject = new MessageObject() { IsTransactional = false, Message = message };
                                }
                            }

                            OnMessageReceived(this, args);
                        }
                        else
                        {
                            this.BasicLog.Log(String.Format("[MqReader] Cannot Read Queue! Queue '{0}' at '{1}'!",
                                Config.QueueName
                               , Config.GetServerName()));
                            break; //stoping the reader
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.BasicLog.Log(ex);
                    OnErrorWhileReceiving(this, new ErrorWhileReceivingEventArgs() { Exception = ex, Config = Config });
                }
            }
            this.Reading = false;
            this.BasicLog.Log(String.Format("[MqReader] Reader Stopped! Queue '{0}' at '{1}'!",
                               Config.QueueName
                               , Config.GetServerName()));
            OnReaderStopped(this, Config);
        }



        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        Message getMessage(MessageQueue queue, MessageQueueTransaction transaction = null)
        {
            Message message = null;

            if (Config.IsAsync)
            {
                if (transaction != null)
                {
                    message = queue.Receive(transaction);
                    this.BasicLog.Log(String.Format("[MqReader] Message Received with Transaction! Queue '{0}' at '{1}', [Message:(Label:'{2}' || MESSAGEID: '{3}')]",
                       Config.QueueName
                      , Config.GetServerName()
                      , message.Label
                      , message.Id));
                }
                else
                {
                    message = queue.Receive();
                    this.BasicLog.Log(String.Format("[MqReader] Message Received! Queue '{0}' at '{1}', [Message:(Label:'{2}' || MESSAGEID: '{3}')]",
                       Config.QueueName
                      , Config.GetServerName()
                      , message.Label
                      , message.Id));
                }

            }
            else
            {
                message = queue.Peek();
                this.BasicLog.Log(String.Format("[MqReader] Message Peeked! Queue '{0}' at '{1}', [Message:(Label:'{2}' || MESSAGEID: '{3}')]",
                        Config.QueueName
                       , Config.GetServerName()
                       , message.Label
                       , message.Id));
            }

            if (message == null)
                throw new Exception(String.Format("[MqReader] Unable to '{0}' Message! Queue '{1}' at '{2}'!",
                    Config.IsAsync ? "Receive" : "Peek"
                   , Config.QueueName
                   , Config.GetServerName()));

            return message;
        }



    }
}
