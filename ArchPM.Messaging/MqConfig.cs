using System;
using System.ComponentModel;
using System.Messaging;
using ArchPM.Core.Extensions;
using ArchPM.Messaging.Infrastructure;

namespace ArchPM.Messaging
{
    /// <summary>
    /// Msmq Custom Config
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class MqConfig : INotifyPropertyChanged
    {
        /// <summary>
        /// The localhost
        /// </summary>
        public const String LOCALHOST = "127.0.0.1";

        /// <summary>
        /// Initializes a new instance of the <see cref="MqConfig"/> class.
        /// </summary>
        public MqConfig()
        {
            this.QueueLabel = "ArchPM";
            this.QueueName = "ArchPM_MSMQ_LOG";
            this.QueueFormatter = new BinaryMessageFormatter();
            this.MessageReceivedCommandHandler = new DefaultMessageReceivedCommandHandler();
            this.IsAsync = true;
            this.ErrorTryCount = 1;
        }

        /// <summary>
        /// Gets and Sets the queue name. Default: ArchPM_MSMQ_LOG
        /// </summary>
        /// <value>
        /// The name of the queue.
        /// </value>
        public String QueueName
        {
            get { return queueName; }
            set
            {
                queueName = value;
                this.ActivePath = GetActivePath();
                PropertyChanged(this, new PropertyChangedEventArgs("QueueName"));
            }
        }
        /// <summary>
        /// The queue name
        /// </summary>
        String queueName;

        /// <summary>
        /// Gets and Sets the Queue Label Default: ArchPM
        /// </summary>
        /// <value>
        /// The queue label.
        /// </value>
        public String QueueLabel { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is asynchronous.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is asynchronous; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsAsync { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is using DNS names or not.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is using DNS; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsUsingDNS { get; set; }

        /// <summary>
        /// Gets and Sets the IP of the remote server Default: EmptyString
        /// </summary>
        /// <value>
        /// The remote server ip.
        /// </value>
        public String RemoteServerIp
        {
            get { return remoteServerIp; }
            set
            {
                remoteServerIp = value;
                this.ActivePath = GetActivePath();
                PropertyChanged(this, new PropertyChangedEventArgs("RemoteServerIp"));
            }
        }
        /// <summary>
        /// The remote server ip
        /// </summary>
        String remoteServerIp;

        /// <summary>
        /// Gets the Active Path
        /// </summary>
        /// <value>
        /// The active path.
        /// </value>
        public String ActivePath
        {
            get
            {
                if (activePath == null)
                    activePath = GetActivePath();
                return activePath;
            }
            private set
            {
                activePath = value;
            }

        }
        /// <summary>
        /// The active path
        /// </summary>
        String activePath;

        /// <summary>
        /// Gets or sets the error try count.
        /// </summary>
        /// <value>
        /// The error try count.
        /// </value>
        public Int32 ErrorTryCount { get; set; }

        /// <summary>
        /// Gets and Sets the formatter of the queue. Default: XmlMessageFormatter
        /// </summary>
        /// <value>
        /// The queue formatter.
        /// </value>
        public IMessageFormatter QueueFormatter { get; set; }

        /// <summary>
        /// Gets Message Reveived Business Command
        /// </summary>
        /// <value>
        /// The message received command handler.
        /// </value>
        public MessageReceivedCommandHandler MessageReceivedCommandHandler { get; private set; }

        /// <summary>
        /// Register Message Reveived Command class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RegisterType<T>() where T : MessageReceivedCommandHandler, new()
        {
            T handler = new T();
            this.MessageReceivedCommandHandler = handler;
        }

        /// <summary>
        /// Returns the active path according to remove or not
        /// </summary>
        /// <returns></returns>
        public String GetActivePath()
        {
            //TODO: change it. dont use if 
            var remotePath = String.Format("FORMATNAME:DIRECT=OS:{0}\\Private$\\{1}", this.RemoteServerIp, this.QueueName);
            if (this.IsUsingDNS)
            {
                remotePath = remotePath.Replace("TCP", "OS");
            }

            var localPath = String.Format(".\\Private$\\{0}", this.QueueName);
            var activePath = this.IsRemoteQueue() ? remotePath : localPath;

            return activePath;
        }

        /// <summary>
        /// Gets whether it is remote or not
        /// </summary>
        /// <returns></returns>
        public Boolean IsRemoteQueue()
        {
            return !String.IsNullOrEmpty(this.RemoteServerIp);
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Gets the name of the server.
        /// </summary>
        /// <returns></returns>
        public String GetServerName()
        {
            return String.IsNullOrEmpty(this.RemoteServerIp) ? LOCALHOST : this.RemoteServerIp;
        }

    }
}
