using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Threading.Tasks;
using ArchPM.Core;
using ArchPM.Core.Extensions;
using ArchPM.Messaging.Infrastructure;
using ArchPM.Messaging.MqLog.Infrastructure;
using ArchPM.Core.Extensions.Advanced;

namespace ArchPM.Messaging.MqLog
{
    /// <summary>
    /// Mq Log client class
    /// </summary>
    /// <seealso cref="ArchPM.Messaging.MqClient" />
    public sealed class MqLogClient : MqClient
    {
        /// <summary>
        /// The lo g_ label
        /// </summary>
        const String LOG_LABEL = "log";
        /// <summary>
        /// The package container
        /// </summary>
        readonly TempObjectPackageContainer<MqLogMessageDTO> packageContainer;
        /// <summary>
        /// Gets the catched exceptions
        /// </summary>
        /// <value>
        /// The catched exceptions.
        /// </value>
        public List<Exception> CatchedExceptions { get; private set; } //todo: sil sonra. log at
        /// <summary>
        /// Transactional logs dispose timespan. Default is 24 hours
        /// </summary>
        /// <value>
        /// The transactional log expiration timespan.
        /// </value>
        public TimeSpan TransactionalLogExpirationTimespan { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MqLogClient"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <exception cref="System.Exception">[MqLogClient] MsmqManager is failed while initializing. check TraceLog</exception>
        public MqLogClient(MqLogConfig config)
            : base(config)
        {
            try
            {
                this.TransactionalLogExpirationTimespan = new TimeSpan(24, 0, 0);

                packageContainer = new TempObjectPackageContainer<MqLogMessageDTO>();
                this.CatchedExceptions = new List<Exception>();
            }
            catch (Exception ex)
            {
                this.BasicLog.Log(ex);
                this.CatchedExceptions.Add(ex);
                throw new Exception("[MqLogClient] MsmqManager is failed while initializing. check TraceLog", ex);
            }
        }


        #region Logs

        /// <summary>
        /// Creates a msmq message for Insert Operation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Task<Boolean> InsertLogAsync<T>(T entity) where T : class
        {
            return Log(entity, MqLogMessageTypes.Insert);
        }

        /// <summary>
        /// Creates a msmq message for Update Operation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Task<Boolean> UpdateLogAsync<T>(T entity) where T : class
        {
            return Log(entity, MqLogMessageTypes.Update);
        }

        /// <summary>
        /// Creates a msmq message for Delete Operation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Task<Boolean> DeleteLogAsync<T>(T entity) where T : class
        {
            return Log(entity, MqLogMessageTypes.Delete);
        }

        /// <summary>
        /// Creates a msmq message for Exceptions
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Task<Boolean> ExceptionLogAsync<T>(T entity) where T : Exception //sacma ama kalsin.
        {
            return Log(entity, MqLogMessageTypes.Exception);
        }

        /// <summary>
        /// Logs the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <returns></returns>
        public Task<Boolean> Log<T>(T entity, MqLogMessageTypes messageType) where T : class
        {
            var task = Task.Run<Boolean>(() =>
            {
                Boolean result = false;
                try
                {
                    entity.ThrowExceptionIfNull("entity");

                    MqLogMessageDTO messageDto = createMessageDto(entity, messageType);

                    this.Push(LOG_LABEL, messageDto);

                    this.BasicLog.Log(String.Format("[MqLogClient] '{0}' Pushed", messageDto.TBYEntityName));

                    result = true;
                }
                catch (Exception ex)
                {
                    this.BasicLog.Log(String.Format("[MqLogClient] LOG FAILED! {0}", ex.GetAllMessages()));

                    if (entity is GlobalMqLogException)
                    {
                        this.BasicLog.Log(ex);
                        this.CatchedExceptions.Add(ex);
                    }
                    else
                    {
                        var tempEx = new Exception("[MqLogClient] MsmqServiceClient.Log exception Occured!", ex);
                        GlobalMqLogException global = new GlobalMqLogException();
                        global.EntityName = entity.GetType().Name;
                        global.ExceptionMessageString = tempEx.GetAllMessages();
                        global.MessageDtoString = "";
                        //log execution
                        Log(global, MqLogMessageTypes.Exception); //todo: bu ne amk
                    }
                }

                return result;
            });

            return task;
        }

        #region Transactional
        /// <summary>
        /// Adds the in package.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="packageId">The package identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <returns></returns>
        public Task<Boolean> AddInPackage<T>(Guid packageId, T entity, MqLogMessageTypes messageType) where T : class
        {
            var task = Task.Run<Boolean>(() =>
            {
                Boolean taskResult = false;
                try
                {
                    MqLogMessageDTO messageDto = createMessageDto(entity, messageType);
                    packageContainer.AddPackage(packageId, messageDto);

                    taskResult = true;
                }
                catch (Exception ex)
                {
                    this.BasicLog.Log(ex);
                    this.CatchedExceptions.Add(ex);
                }

                return taskResult;
            });
            return task;
        }

        /// <summary>
        /// Packages the commit.
        /// </summary>
        /// <param name="packageId">The package identifier.</param>
        /// <returns></returns>
        public Task<Boolean> PackageCommit(Guid packageId)
        {
            var task = Task.Run<Boolean>(() =>
            {
                Boolean taskResult = true;
                MessageQueueTransaction trans = null;
                try
                {
                    var package = packageContainer.GetPackage(packageId);
                    var list = package.Data;

                    using (trans = new MessageQueueTransaction())
                    {
                        trans.Begin();
                        foreach (var item in list)
                        {
                            base.Push(LOG_LABEL, item);
                        }
                        trans.Commit();
                        this.BasicLog.Log(String.Format("[MqLogClient] Transaction Committed! PackageId:{0}", packageId));
                    }
                }
                catch (Exception ex)
                {
                    trans.Abort();
                    this.BasicLog.Log(ex);
                    this.CatchedExceptions.Add(ex);
                    taskResult = false;
                }
                finally
                {
                    packageContainer.RemovePackage(packageId);
                }

                return taskResult;
            });
            return task;
        }

        /// <summary>
        /// Packages the rollback.
        /// </summary>
        /// <param name="packageId">The package identifier.</param>
        /// <returns></returns>
        public Task<Boolean> PackageRollback(Guid packageId)
        {
            var task = Task.Run<Boolean>(() =>
            {
                Boolean taskResult = true;
                try
                {
                    var itemCount = packageContainer.RemovePackage(packageId);
                    this.BasicLog.Log(String.Format("[MqLogClient] Transaction Rollback! Total {1} items Removed!  roContainerId:{0}", packageId, itemCount));
                }
                catch (Exception ex)
                {
                    this.BasicLog.Log(ex);
                    this.CatchedExceptions.Add(ex);
                    taskResult = false;
                }

                return taskResult;
            });
            return task;
        }

        #endregion

        #endregion


        /// <summary>
        /// Clears the expired transactional logs.
        /// </summary>
        /// <returns></returns>
        public Task<Boolean> ClearExpiredTransactionalLogs()
        {
            var task = Task.Run<Boolean>(() =>
            {
                Boolean taskResult = false;
                try
                {
                    var itemCount = packageContainer.RemoveExpiredPackages(this.TransactionalLogExpirationTimespan);
                    this.BasicLog.Log(String.Format("[MqLogClient] Expired Transactions Rollback! Total {0} items Removed!", itemCount));
                    taskResult = true;
                }
                catch (Exception ex)
                {
                    this.BasicLog.Log(ex);
                    this.CatchedExceptions.Add(ex);
                }

                return taskResult;
            });
            return task;
        }

        /// <summary>
        /// Clears the catched exceptions.
        /// </summary>
        public void ClearCatchedExceptions()
        {
            this.CatchedExceptions.Clear();
        }

        /// <summary>
        /// Creates new MessageDTO object from given entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <returns></returns>
        MqLogMessageDTO createMessageDto<T>(T entity, MqLogMessageTypes messageType) where T : class
        {
            MqLogMessageDTO messageDto = new MqLogMessageDTO();

            messageDto.TBYEntityName = typeof(T).AnonymousSupportedTypeName();
            messageDto.TBYMessageType = messageType;
            messageDto.Properties = entity.Properties().ToList();

            return messageDto;
        }

        

    }

}
