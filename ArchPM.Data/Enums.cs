namespace ArchPM.Data
{
    /// <summary>
    /// 
    /// </summary>
    public enum ApplicationEnvironments
    {
        /// <summary>
        /// Test Environment: 0
        /// </summary>
        Test,

        /// <summary>
        /// Development Environment: 1
        /// </summary>
        Development,

        /// <summary>
        /// Production Environment: 2
        /// </summary>
        Production
    }

    /// <summary>
    /// 
    /// </summary>
    public enum EntityStatus
    {
        /// <summary>
        /// The passive
        /// </summary>
        Passive = 0,
        /// <summary>
        /// The active
        /// </summary>
        Active = 1,
        /// <summary>
        /// The deleted
        /// </summary>
        Deleted = 2
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ResponseStatusTypes
    {
        /// <summary>
        /// 0
        /// </summary>
        Failed,
        /// <summary>
        /// 1
        /// </summary>
        OK
    }
}
