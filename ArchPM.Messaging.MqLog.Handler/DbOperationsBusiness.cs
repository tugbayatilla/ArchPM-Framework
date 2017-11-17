using System;
using ArchPM.Core;
using ArchPM.Messaging.Infrastructure;
using ArchPM.Messaging.MqLog.DbBusiness.Infrastructure;
using ArchPM.Messaging.MqLog.Infrastructure;


namespace ArchPM.Messaging.MqLog.DbBusiness
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DbOperationsBusiness
    {
        /// <summary>
        /// The database operations
        /// </summary>
        readonly IDbOperations dbOperations;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbOperationsBusiness"/> class.
        /// </summary>
        /// <param name="databaseManager">The database manager.</param>
        public DbOperationsBusiness(IDbOperations databaseManager)
        {
            databaseManager.ThrowExceptionIfNull("databaseManager");

            this.dbOperations = databaseManager;
        }

        /// <summary>
        /// Executes the specified message object.
        /// </summary>
        /// <param name="messageObject">The message object.</param>
        /// <exception cref="System.Exception">Message body is not a MqLogMessageDTO</exception>
        public void Execute(MessageObject messageObject)
        {
            var messageDTO = messageObject.Message.Body as MqLogMessageDTO;
            if (messageDTO == null)
                throw new Exception("Message body is not a MqLogMessageDTO");


            //table
            dbOperations.CreateTableIfNotExist(messageDTO);

            //fields
            dbOperations.CreateFieldsIfNotExist(messageDTO);

            //persist
            dbOperations.PersistItem(messageDTO);
        }

    }

}
