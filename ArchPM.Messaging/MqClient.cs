using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.ServiceProcess;
using ArchPM.Core;
using ArchPM.Core.Extensions;
using ArchPM.Core.Logging.BasicLogging;

namespace ArchPM.Messaging
{
    /// <summary>
    /// 
    /// </summary>
    public class MqClient
    {
        /// <summary>
        /// Gets the MqConfig
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public MqConfig Config { get; private set; }

        public IBasicLog BasicLog { get; set; }

        /// <summary>
        /// the Constructor of the
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <exception cref="System.Exception">[MqClient] MsmqManager is Failed While Initializing. Check TraceLog</exception>
        public MqClient(MqConfig config)
        {
            try
            {
                this.BasicLog = new NullBasicLog();
                config.ThrowExceptionIfNull("[MqClient] MsmqConfig is null. Need at least one MqConfig Instance");
                this.Config = config;

                //check service availability
                //this.checkLocalMsmqServiceAvailable(); fistan: just checking...
                this.CreateLocalMessageQueueIfNotExist();
            }
            catch (Exception ex)
            {
                this.BasicLog.Log(ex);
                throw new Exception("[MqClient] MsmqManager is Failed While Initializing. Check TraceLog", ex);
            }
        }

        /// <summary>
        /// Pushes the specified message label.
        /// </summary>
        /// <param name="messageLabel">The message label.</param>
        /// <param name="messageBodies">The message bodies.</param>
        public void Push(String messageLabel, params Object[] messageBodies)
        {
            this.QueueHandler(
                transactional: (queue, transaction) =>
                {
                    #region transactional
                    var tempList = new List<Message>();
                    foreach (var body in messageBodies)
                    {
                        using (Message message = new Message())
                        {
                            message.Label = messageLabel;
                            message.Body = body;
                            message.Recoverable = true;
                            message.Formatter = this.Config.QueueFormatter;

                            //send message to queue
                            queue.Send(message, transaction);
                            tempList.Add(message);
                        }
                    }
                    Int32 tempCount = 1;
                    foreach (var message in tempList)
                    {
                        this.BasicLog.Log(String.Format("[MqClient] Message Sent with MessageQueueTransaction [{4} of {5}]! Queue '{0}' at '{1}', [Message:(Label:'{2}' || MESSAGEID: '{3}')]",
                               this.Config.QueueName
                               , this.Config.GetServerName()
                               , message.Label
                               , message.Id
                               , tempCount++
                               , tempList.Count)); 
                    }
                    tempList = null;

                    #endregion
                },
                direct: (queue) =>
                {
                    #region Direct

                    foreach (var body in messageBodies)
                    {
                        using (Message message = new Message())
                        {
                            message.Label = messageLabel;
                            message.Body = body;
                            message.Recoverable = true;
                            message.Formatter = this.Config.QueueFormatter;

                            //send message to queue
                            queue.Send(message);

                            this.BasicLog.Log(String.Format("[MqClient] Message Sent! Queue '{0}' at '{1}', [Message:(Label:'{2}' || MESSAGEID: '{3}')]",
                                this.Config.QueueName
                                , this.Config.GetServerName()
                                , message.Label
                                , message.Id));
                        }
                    }

                    #endregion
                },
                exception: (ex) =>
                {
                    this.BasicLog.Log(String.Format("[MqClient] Message Send FAILED! Queue '{0}' at '{1}', Because {2}",
                            this.Config.QueueName
                            , this.Config.GetServerName()
                            , ex.GetAllMessages()));
                });
        }

        /// <summary>
        /// Resends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Resend(Message message)
        {
            message.AppSpecific += 1;
            if (message.AppSpecific >= Config.ErrorTryCount)
            {
                this.BasicLog.Log(String.Format("[MqClient] Max TryCount Reached [{4} of {5}]! Message Resend Stopped! Queue '{0}' at '{1}', [Message:(Label:'{2}' || MESSAGEID: '{3}')]",
                                this.Config.QueueName
                                , this.Config.GetServerName()
                                , message.Label
                                , message.Id
                                , message.AppSpecific
                                , Config.ErrorTryCount));

                RemoveMessage(message);

                return;
            }

            using (message)
            {
                this.QueueHandler(
                    transactional: (queue, transaction) =>
                    {
                        //send message to queue
                        queue.Send(message, transaction);
                        this.BasicLog.Log(String.Format("[MqClient] Message Resend with transaction! Queue '{0}' at '{1}', [Message:(Label:'{2}' || MESSAGEID: '{3}')]",
                                this.Config.QueueName
                                , this.Config.GetServerName()
                                , message.Label
                                , message.Id));

                    },
                    direct: (queue) =>
                    {
                        queue.Send(message);
                        this.BasicLog.Log(String.Format("[MqClient] Message Resend! Queue '{0}' at '{1}', [Message:(Label:'{2}' || MESSAGEID: '{3}')]",
                                this.Config.QueueName
                                , this.Config.GetServerName()
                                , message.Label
                                , message.Id));
                    },
                    exception: (ex) =>
                    {
                        this.BasicLog.Log(String.Format("[MqClient] Message Resend FAILED! Queue '{0}' at '{1}', [Message:(Label:'{2}' || MESSAGEID: '{3}')]. Because {4}",
                                this.Config.QueueName
                                , this.Config.GetServerName()
                                , message.Label
                                , message.Id
                                , ex.GetAllMessages()));
                    });
            }

        }

        /// <summary>
        /// Removes the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void RemoveMessage(Message message)
        {
            //when message peeked, must be removed from queue. only sync operations can peek, otherwise received
            if (this.Config.IsAsync)
                return;

            using (message)
            {
                this.QueueHandler(
                    transactional: (queue, transaction) =>
                    {
                        queue.ReceiveById(message.Id, transaction);

                        this.BasicLog.Log(String.Format("[MqClient] Message Removed! Queue '{0}' at '{1}' with transaction, [Message:(Label:'{2}' || MESSAGEID: '{3}')]",
                            this.Config.QueueName
                            , this.Config.GetServerName()
                            , message.Label
                            , message.Id));
                    },
                    direct: (queue) =>
                    {
                        queue.ReceiveById(message.Id);
                        this.BasicLog.Log(String.Format("[MqClient] Message Removed! Queue '{0}' at '{1}' with transaction, [Message:(Label:'{2}' || MESSAGEID: '{3}')]",
                            this.Config.QueueName
                           , this.Config.GetServerName()
                           , message.Label
                           , message.Id));
                    },
                    exception: (ex) =>
                    {
                        this.BasicLog.Log(String.Format("[MqClient] Message Removed! Queue '{0}' at '{1}' with transaction, [Message:(Label:'{2}' || MESSAGEID: '{3}')]. Because {4}",
                             this.Config.QueueName
                           , this.Config.GetServerName()
                           , message.Label
                           , message.Id
                           , ex.GetAllMessages()));
                    });
            }

        }

        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public IEnumerable<Message> GetMessages(Func<Message, Boolean> predicate = null)
        {
            using (MessageQueue queue = new MessageQueue(this.Config.ActivePath))
            {
                if (predicate == null)
                    return queue.GetAllMessages().ToList();
                else
                    return queue.GetAllMessages().Where(predicate);
            }
        }

        /// <summary>
        /// Check if using local messaging queue
        /// </summary>
        /// <returns></returns>
        Boolean checkIfLocalMessageQueueExist()
        {
            return MessageQueue.Exists(this.Config.ActivePath);
        }
        /// <summary>
        /// Checks service availability. ot means it is working or not. if not returns false otherwise return true.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <exception cref="System.Exception">[MqClient] MSMQ Service is Avaiable but It is NOT Running!
        /// or
        /// [MqClient] MSMQ Service is Available!</exception>
        public static void CheckLocalMsmqServiceAvailable(MqConfig config)
        {
            try
            {
                if (config.IsRemoteQueue())
                    return;

                List<ServiceController> services = ServiceController.GetServices().ToList();
                ServiceController msQue = services.Find(o => o.ServiceName == "MSMQ");
                if (msQue != null)
                {
                    if (msQue.Status != ServiceControllerStatus.Running)
                    {
                        // It is not running.
                        throw new Exception("[MqClient] MSMQ Service is Avaiable but It is NOT Running!");
                    }
                }
                else
                {
                    /* Not installed? */
                    throw new Exception("[MqClient] MSMQ Service is Available!");
                }
            }
            catch (Exception ex)
            {
                Exception temp = new Exception(String.Format("[MqClient] checkLocalMsmqServiceAvailable Failed! Queue '{0}' at '{1}'",
                    config.QueueName
                  , config.GetServerName()), ex);
                throw temp;
            }
            
        }
        /// <summary>
        /// Check if message queue is created
        /// </summary>
        void CreateLocalMessageQueueIfNotExist()
        {
            try
            {
                if (Config.IsRemoteQueue())
                    return;

                var localQueueExist = checkIfLocalMessageQueueExist();
                if (localQueueExist)
                    return;

                //todo: transational queue yaratmak istersek buradan yaratacagiz. ancak testleri iyi yapilmali.
                var queue = MessageQueue.Create(this.Config.ActivePath);

                queue.Label = Config.QueueLabel;
                setPermissions(queue);

                this.BasicLog.Log(String.Format("[MqClient] Local Queue is Created! Queue '{0}' at '{1}'!",
                                 this.Config.QueueName
                               , this.Config.GetServerName()));
            }
            catch (Exception ex)
            {
                Exception temp = new Exception(String.Format("[MqClient] createLocalMessageQueueIfNotExist Failed! Queue '{0}' at '{1}'", 
                    this.Config.QueueName
                  , this.Config.GetServerName()), ex);
                this.BasicLog.Log(temp);
                throw temp;
            }
            

        }
        /// <summary>
        /// Sets the permissions.
        /// </summary>
        /// <param name="queue">The queue.</param>
        void setPermissions(MessageQueue queue)
        {
            try
            {
                queue.SetPermissions(
                    "Everyone",
                    MessageQueueAccessRights.FullControl,
                    AccessControlEntryType.Allow);

                this.BasicLog.Log(String.Format("[MqClient] Permission set to 'Everyone' as FullControl! Queue '{0}' at '{1}'!"
                    , this.Config.QueueName
                    , this.Config.GetServerName()));

                queue.SetPermissions(
                    "ANONYMOUS LOGON",
                    MessageQueueAccessRights.FullControl,
                    AccessControlEntryType.Allow);

                this.BasicLog.Log(String.Format("[MqClient] Permission set to 'ANONYMOUS LOGON' as FullControl! Queue '{0}' at '{1}'!"
                    , this.Config.QueueName
                    , this.Config.GetServerName()));

            }
            catch (Exception ex)
            {
                Exception temp = new Exception(String.Format("[MqClient] setPermissions Failed! Queue '{0}' at '{1}'",
                    this.Config.QueueName
                  , this.Config.GetServerName()), ex);
                this.BasicLog.Log(temp);
                throw temp;
            }
            
        }

        /// <summary>
        /// Queues the handler.
        /// </summary>
        /// <param name="transactional">The transactional.</param>
        /// <param name="direct">The direct.</param>
        /// <param name="exception">The exception.</param>
        public void QueueHandler(Action<MessageQueue, MessageQueueTransaction> transactional, Action<MessageQueue> direct, Action<Exception> exception)
        {
            MessageQueueTransaction transaction = null;
            try
            {
                using (MessageQueue queue = new MessageQueue(this.Config.ActivePath))
                {
                    queue.MessageReadPropertyFilter.SetAll();
                    queue.Formatter = Config.QueueFormatter;

                    //cannot get transactional property while working on remote server.
                    if (this.Config.IsRemoteQueue())
                    {
                        direct(queue);
                    }
                    else
                    {
                        if (queue.Transactional)
                        {
                            using (transaction = new MessageQueueTransaction())
                            {
                                transaction.Begin();
                                transactional(queue, transaction);
                                transaction.Commit();
                            }
                        }
                        else
                        {
                            direct(queue);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (!this.Config.IsRemoteQueue() && transaction != null)
                    transaction.Abort();

                exception(ex);
            }
        }

    }
}