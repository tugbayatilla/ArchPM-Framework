using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Threading.Tasks;
using ArchPM.Core;
using ArchPM.Core.Extensions;
using ArchPM.Core.Notifications;
using ArchPM.Messaging.Infrastructure;
using ArchPM.Messaging.Infrastructure.EventArg;

namespace ArchPM.Messaging
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class MqServer : IDisposable
    {
        #region Members & Properties & Events

        /// <summary>
        /// Gets a value indicating whether this <see cref="MqServer"/> is running.
        /// </summary>
        /// <value>
        ///   <c>true</c> if running; otherwise, <c>false</c>.
        /// </value>
        public Boolean Running { get; private set; }

        /// <summary>
        /// The timer
        /// </summary>
        readonly System.Timers.Timer timer;

        /// <summary>
        /// The timer elapsed
        /// </summary>
        TimerElapsedTimes timerElapsed;

        /// <summary>
        /// The mq configs
        /// </summary>
        protected readonly List<MqConfig> mqConfigs;

        /// <summary>
        /// The mq readers
        /// </summary>
        protected readonly List<MqReader> mqReaders;

        /// <summary>
        /// Occurs when [on message operated].
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs> OnMessageOperated = delegate { };

        public INotification Notification { get; set; }

        /// <summary>
        /// Gets and Sets the timer elapsed. Default: 30min
        /// </summary>
        /// <value>
        /// The timer elapsed.
        /// </value>
        public TimerElapsedTimes TimerElapsed
        {
            get { return timerElapsed; }
            set
            {
                var old = this.timerElapsed;
                this.timerElapsed = value;

                if (this.timer != null)
                {
                    this.timer.Interval = (Double)value;
                    this.Notification.Notify(String.Format("[MqServer] Timer ElapsedTime Changed '{0}' to '{1}'", old, value));
                }
            }
        }
        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="MqServer"/> class.
        /// </summary>
        /// <param name="configs">The configs.</param>
        public MqServer(params MqConfig[] configs)
        {
            this.Notification = new NullNotification();

            configs.NotEmpty();
            checkConfigurations(configs);
            checkCommandHandlers(configs);

            //default value of timer
            this.timerElapsed = TimerElapsedTimes.min30;
            //configs
            this.mqConfigs = new List<MqConfig>(configs);
            this.mqReaders = new List<MqReader>();

            //timer initialize
            this.timer = new System.Timers.Timer((Int32)this.TimerElapsed);
            timer.Elapsed += timer_Elapsed;

            this.Running = false;
        }

        #region Publics

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            if (this.Running)
            {
                this.Notification.Notify(String.Format("[MqServer] MqServer is already running..."));
                return;
            }

            foreach (var config in this.mqConfigs)
            {
                var task = Task.Run(() =>
                {
                    var reader = new MqReader(config);
                    reader.OnMessageReceived += reader_OnMessageReceived;
                    reader.OnErrorWhileReceiving += reader_OnErrorWhileReceiving;
                    reader.OnReaderStopped += reader_OnReaderStopped;
                    reader.StartReading();

                    mqReaders.Add(reader);
                });
            }

            //start timer
            this.timer.Start();
            this.Notification.Notify(String.Format("[MqServer] Timer for MqReader is started! Interval is {0}", this.TimerElapsed));

            this.Running = true;
            this.Notification.Notify(String.Format("[MqServer] MqServer is started!"));
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Notification.Notify(String.Format("[MqServer] Disposing..."));

            foreach (var reader in this.mqReaders)
            {
                reader.OnMessageReceived -= reader_OnMessageReceived;
                reader.OnErrorWhileReceiving -= reader_OnErrorWhileReceiving;
                reader.OnReaderStopped -= reader_OnReaderStopped;
            }

            timer.Elapsed -= timer_Elapsed;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the OnErrorWhileReceiving event of the reader control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ErrorWhileReceivingEventArgs"/> instance containing the event data.</param>
        void reader_OnErrorWhileReceiving(object sender, ErrorWhileReceivingEventArgs e)
        {
            try
            {
                this.Notification.Notify(String.Format("[MqServer] Error While Receiving! Queue '{0}' at '{1}'! Because: {2}",
                                   e.Config.QueueName
                                   , e.Config.GetServerName()
                                   , e.Exception.GetAllMessages()));
                //must be logged here
            }
            catch { }
        }

        /// <summary>
        /// Reader_s the on reader stopped.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void reader_OnReaderStopped(object sender, MqConfig e)
        {
            this.Notification.Notify(String.Format("[MqServer] Reader Stopped! Queue '{0}' at '{1}'!",
                               e.QueueName
                               , e.GetServerName()));
        }

        /// <summary>
        /// Handles the OnMessageReceived event of the reader control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MessageReceivedEventArgs"/> instance containing the event data.</param>
        void reader_OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            #region Result Handle
            Action<MessageReceivedBusinessHandlerResultTypes> resultHandle = (result) =>
            {
                switch (result)
                {
                    case MessageReceivedBusinessHandlerResultTypes.Success:
                        {
                            this.Notification.Notify(String.Format("[MqServer] Success Command Received! Queue '{0}' at '{1}'!",
                                e.Config.QueueName, e.Config.GetServerName()));

                            if (e.MessageObject.IsTransactional)
                            {
                                e.MessageObject.Transaction.Commit();
                                this.Notification.Notify(String.Format("[MqServer] Transaction Committed! Queue '{0}' at '{1}'!",
                                   e.Config.QueueName, e.Config.GetServerName()));
                            }

                            this.RemoveMessage(e.Config, e.MessageObject.Message);
                        }
                        break;
                    case MessageReceivedBusinessHandlerResultTypes.Retry:
                        {
                            this.Notification.Notify(String.Format("[MqServer] Retry Command Received! Queue '{0}' at '{1}'!",
                                e.Config.QueueName, e.Config.GetServerName()));

                            this.ResendMessage(e.Config, e.MessageObject.Message);
                        }
                        break;
                    case MessageReceivedBusinessHandlerResultTypes.DeleteAllRelated:
                        {
                            this.Notification.Notify(String.Format("[MqServer] DeleteAllRelated Command Received! Queue '{0}' at '{1}'!",
                                e.Config.QueueName, e.Config.GetServerName()));

                            RemoveAllSameLabelMessages(e.Config, e.MessageObject.Message);
                        }
                        break;
                    case MessageReceivedBusinessHandlerResultTypes.RemoveMessage:
                        {
                            this.Notification.Notify(String.Format("[MqServer] RemoveMessage Command Received! Queue '{0}' at '{1}'!",
                                e.Config.QueueName, e.Config.GetServerName()));

                            RemoveMessage(e.Config, e.MessageObject.Message);
                        }
                        break;
                    default:
                        break;
                }

            };
            #endregion
            if (e.Config.IsAsync)
            {
                #region Async
                Task task = Task.Run(() =>
                {
                    try
                    {
                        var handler = e.Config.MessageReceivedCommandHandler;
                        var result = handler.Execute(e);

                        resultHandle(result);
                    }
                    catch (Exception ex)
                    {
                        String message = String.Format("[MqServer] Async Handler Exception! Queue '{0}' at '{1}'!",
                                e.Config.QueueName, e.Config.GetServerName());
                        Exception tempEx = new Exception(message, ex);
                        this.Notification.Notify(tempEx);
                    }
                    finally
                    {
                        OnMessageOperated(this, e);
                    }
                }).ContinueWith((t) =>
                {
                    t.Dispose();
                });

                #endregion
            }
            else
            {
                #region Sync

                MessageReceivedCommandHandler handler = null;
                try
                {
                    handler = e.Config.MessageReceivedCommandHandler;
                    var result = handler.Execute(e);

                    resultHandle(result);
                }
                catch (Exception ex)
                {
                    String message = String.Format("[MqServer] Sync Handler Exception! Queue '{0}' at '{1}'!",
                                e.Config.QueueName, e.Config.GetServerName());
                    Exception tempEx = new Exception(message, ex);
                    this.Notification.Notify(tempEx);

                    //fistan: burasi onemli. business'ten sync durumunda hatali bir durum geldiginde peek durumu oldugundan surekli donguye giriyor.
                    //ya hatali paket silinecek ya da kadir'in onerisi ile tum paketler silinecek.
                    //bunun uzerine dusunulmesi lazim.
                    ResendMessage(e.Config, e.MessageObject.Message);
                }
                finally
                {
                    OnMessageOperated(this, e);
                }
                #endregion
            }

        }

        /// <summary>
        /// Handles the Elapsed event of the timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var stoppedReaders = mqReaders.Where(p => p.Reading == false);
            if (stoppedReaders.Count() <= 0)
                return;

            foreach (var item in stoppedReaders)
            {
                item.StartReading();
                this.Notification.Notify(String.Format("[MqServer] Restarting Readers! Queue '{0}' at '{1}'!",
                    item.Config.QueueName, item.Config.GetServerName()));
            }
        }


        #endregion

        #region Privates
        /// <summary>
        /// Checks the configurations.
        /// </summary>
        /// <param name="configs">The configs.</param>
        /// <exception cref="System.Exception">[MqServer] [CHECK] Cannot Use Same Configuration! Change RemoteServerIp, QueueName</exception>
        void checkConfigurations(MqConfig[] configs)
        {
            var dublicate = configs.GroupBy(x => new { x.RemoteServerIp, x.QueueName, x.GetType().FullName }).Any(g => g.Count() > 1);
            if (dublicate)
                throw new Exception("[MqServer] [CHECK] Cannot Use Same Configuration! Change RemoteServerIp, QueueName");
        }
        /// <summary>
        /// Checks the command handlers.
        /// </summary>
        /// <param name="configs">The configs.</param>
        /// <exception cref="System.Exception">[MqServer] [CHECK] All MessageReceivedCommandHandler items must be implemented in config files! one or more config files have null MessageReceivedCommandHandler property!\r\n Null Config QueueNames:  + nullQueueNames</exception>
        void checkCommandHandlers(MqConfig[] configs)
        {
            var nulls = configs.Where(p => p.MessageReceivedCommandHandler == null);
            if (nulls.Count() > 0)
            {
                String nullQueueNames = String.Empty;
                foreach (var item in nulls)
                {
                    nullQueueNames += String.Format("[{0}], ", item.QueueName);
                }

                throw new Exception("[MqServer] [CHECK] All MessageReceivedCommandHandler items must be implemented in config files! one or more config files have null MessageReceivedCommandHandler property!\r\n Null Config QueueNames: " + nullQueueNames);
            }
        }
        #endregion

        #region Protected

        /// <summary>
        /// Resends the message.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="message">The message.</param>
        protected void ResendMessage(MqConfig config, Message message)
        {
            var mqClient = new MqClient(config);
            mqClient.Resend(message);
        }

        /// <summary>
        /// Removes the message.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="message">The message.</param>
        protected void RemoveMessage(MqConfig config, Message message)
        {
            if (config.IsAsync)
                return;

            var mqClient = new MqClient(config);
            mqClient.RemoveMessage(message);
        }

        /// <summary>
        /// Removes all same label messages.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="message">The message.</param>
        protected void RemoveAllSameLabelMessages(MqConfig config, Message message)
        {
            var mqClient = new MqClient(config);
            var sameLabelMessages = mqClient.GetMessages(p => p.Label == message.Label);
            foreach (var sameLabelMessage in sameLabelMessages)
            {
                mqClient.RemoveMessage(sameLabelMessage);
            }

            this.Notification.Notify(String.Format("[MqServer] All Related Messages(Sharing Same Label:'{2}') are Removed! Queue '{0}' at '{1}'!",
                config.QueueName,
                config.GetServerName()
              , message.Label));

        }

        #endregion
    }
}
