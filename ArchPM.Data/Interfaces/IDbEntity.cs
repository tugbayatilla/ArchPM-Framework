using System;

namespace ArchPM.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Int32 ID { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        EntityStatus STATUS { get; set; }
        /// <summary>
        /// Gets or sets the process time.
        /// </summary>
        /// <value>
        /// The process time.
        /// </value>
        DateTime PROCESS_TIME { get; set; }
        /// <summary>
        /// Gets or sets the name of the process user.
        /// </summary>
        /// <value>
        /// The name of the process user.
        /// </value>
        String PROCESS_USER_NAME { get; set; }
    }
}
