using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArchPM.Core;
using ArchPM.Messaging.MqLog.Infrastructure;
using ArchPM.Core.Logging.BasicLogging;

namespace ArchPM.Messaging.MqLog
{
    /// <summary>
    /// Singleton class for MqLogClient
    /// </summary>
    public sealed class MqLogClientSingleton
    {
        /// <summary>
        /// creates a single instance of the object
        /// </summary>
        public static readonly MqLogClientSingleton Instance = new MqLogClientSingleton();

        /// <summary>
        /// Gets the MqLogClient object
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public MqLogClient Client { get; private set; }

        /// <summary>
        /// Gets the object has been initialized or not
        /// </summary>
        /// <value>
        ///   <c>true</c> if initialized; otherwise, <c>false</c>.
        /// </value>
        public Boolean Initialized { get; private set; }

        public IBasicLog BasicLog { get; set; }


        /// <summary>
        /// Prevents a default instance of the <see cref="MqLogClientSingleton"/> class from being created.
        /// </summary>
        private MqLogClientSingleton()
        {
            this.BasicLog = new NullBasicLog();

            this.Initialized = false;
        }

        #region Asyncs
        /// <summary>
        /// Initialize the object
        /// </summary>
        /// <param name="config">The configuration.</param>
        public void Initialize(MqLogConfig config)
        {
            if (this.Initialized)
                return;

            this.Client = new MqLogClient(config);

            this.Initialized = true;
        }

        /// <summary>
        /// Creates a log object as async with Delete Flag
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        public async void DeleteLogAsync<T>(T entity) where T : class
        {
            checkInitialize();

            var result = await this.Client.DeleteLogAsync<T>(entity);

            this.BasicLog.Log(String.Format("[MqLogClientSingleton] DeleteLog result:{0}", result));
        }

        /// <summary>
        /// Creates a exception log object as async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        public async void ExceptionLogAsync<T>(T entity) where T : Exception
        {
            checkInitialize();
            var result = await this.Client.ExceptionLogAsync<T>(entity);

            this.BasicLog.Log(String.Format("[MqLogClientSingleton] ExceptionLog result:{0}", result));
        }

        /// <summary>
        /// Creates a log object as async with Insert Flag
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public async void InsertLogAsync<T>(T entity) where T : class
        {
            checkInitialize();
            var result = await this.Client.InsertLogAsync<T>(entity);
            this.BasicLog.Log(String.Format("[MqLogClientSingleton] InsertLogAsync result:{0}", result));
        }

        /// <summary>
        /// Creates a log object as async with Update Flag
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        public async void UpdateLogAsync<T>(T entity) where T : class
        {
            checkInitialize();
            var result = await this.Client.UpdateLogAsync<T>(entity);
            this.BasicLog.Log(String.Format("[MqLogClientSingleton] UpdateLogAsync result:{0}", result));
        }

        /// <summary>
        /// Creates a temprorary container to hold the given object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="containerId">The container identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="messageType">Type of the message.</param>
        public async void TransactionalLogAsync<T>(Guid containerId, T entity, MqLogMessageTypes messageType) where T : class
        {
            checkInitialize();
            var result = await this.Client.AddInPackage<T>(containerId, entity, messageType);
            this.BasicLog.Log(String.Format("[MqLogClientSingleton] TransactionalLog result:{0}", result));
        }

        /// <summary>
        /// Commits the temprorary container with a given guid
        /// </summary>
        /// <param name="containerId">The container identifier.</param>
        public async void CommitAsync(Guid containerId)
        {
            checkInitialize();
            var result = await this.Client.PackageCommit(containerId);
            this.BasicLog.Log(String.Format("[MqLogClientSingleton] Commit result:{0}", result));
        }

        /// <summary>
        /// Rollbacks the temprorary container with a given guid
        /// </summary>
        /// <param name="containerId">The container identifier.</param>
        public async void RollbackAsync(Guid containerId)
        {
            checkInitialize();
            var result = await this.Client.PackageRollback(containerId);
            this.BasicLog.Log(String.Format("[MqLogClientSingleton] Rollback result:{0}", result));
        }
        #endregion


        #region Task Results

        /// <summary>
        /// Creates a log object as async with Delete Flag
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Task<Boolean> DeleteLog<T>(T entity) where T : class
        {
            checkInitialize();
            return this.Client.DeleteLogAsync<T>(entity);
        }

        /// <summary>
        /// Creates a exception log object as async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Task<Boolean> ExceptionLog<T>(T entity) where T : Exception
        {
            checkInitialize();
            return this.Client.ExceptionLogAsync<T>(entity);
        }

        /// <summary>
        /// Creates a log object as async with Insert Flag
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Task<Boolean> InsertLog<T>(T entity) where T : class
        {
            checkInitialize();
            return this.Client.InsertLogAsync<T>(entity);
        }

        /// <summary>
        /// Creates a log object as async with Update Flag
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Task<Boolean>  UpdateLog<T>(T entity) where T : class
        {
            checkInitialize();
            return this.Client.UpdateLogAsync<T>(entity);
        }

        /// <summary>
        /// Creates a temprorary container to hold the given object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="containerId">The container identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <returns></returns>
        public Task<Boolean> TransactionalLog<T>(Guid containerId, T entity, MqLogMessageTypes messageType) where T : class
        {
            checkInitialize();
            return this.Client.AddInPackage<T>(containerId, entity, messageType);
        }

        /// <summary>
        /// Commits the temprorary container with a given guid
        /// </summary>
        /// <param name="containerId">The container identifier.</param>
        /// <returns></returns>
        public Task<Boolean>  Commit(Guid containerId)
        {
            checkInitialize();
            return this.Client.PackageCommit(containerId);
        }


        /// <summary>
        /// Rollbacks the temprorary container with a given guid
        /// </summary>
        /// <param name="containerId">The container identifier.</param>
        /// <returns></returns>
        public Task<Boolean>  Rollback(Guid containerId)
        {
            checkInitialize();
            return this.Client.PackageRollback(containerId);
        }
        #endregion

        /// <summary>
        /// Clears the expired Transactional Packages
        /// </summary>
        public void ClearExpiredTransactionalLogs()
        {
            checkInitialize();
            this.Client.ClearExpiredTransactionalLogs();
        }

        /// <summary>
        /// Clears catched exceptions
        /// </summary>
        public void ClearCatchedExceptions()
        {
            checkInitialize();
            this.Client.ClearCatchedExceptions();
        }

        /// <summary>
        /// Gets the Catched Exceptions
        /// </summary>
        /// <value>
        /// The catched exceptions.
        /// </value>
        public List<Exception> CatchedExceptions
        {
            get { return Client.CatchedExceptions; }
        }

        /// <summary>
        /// Checks the initialize.
        /// </summary>
        /// <exception cref="System.Exception">[MqLogClientSingleton] MsmqLog Must be initialized first!</exception>
        void checkInitialize()
        {
            if (!this.Initialized)
                throw new Exception("[MqLogClientSingleton] MsmqLog Must be initialized first!");

        }

    }
}
