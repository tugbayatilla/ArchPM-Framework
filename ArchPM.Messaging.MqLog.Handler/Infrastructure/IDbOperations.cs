using System;
using ArchPM.Messaging.MqLog.Infrastructure;


namespace ArchPM.Messaging.MqLog.DbBusiness.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbOperations
    {
        /// <summary>
        /// Creates the fields if not exist.
        /// </summary>
        /// <param name="dto">The dto.</param>
        void CreateFieldsIfNotExist(MqLogMessageDTO dto);
        /// <summary>
        /// Creates the table if not exist.
        /// </summary>
        /// <param name="dto">The dto.</param>
        void CreateTableIfNotExist(MqLogMessageDTO dto);
        /// <summary>
        /// Persists the item.
        /// </summary>
        /// <param name="dto">The dto.</param>
        void PersistItem(MqLogMessageDTO dto);
        
        //Handle Exception and persist
        /// <summary>
        /// Called when [exception].
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        MqLogMessageDTO OnException(MqLogMessageDTO dto, Exception ex);
    }
}
