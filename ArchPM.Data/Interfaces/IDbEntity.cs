using System;

namespace ArchPM.Data
{
    public interface IDbEntity
    {
        Int32 ID { get; set; }
        EntityStatus STATUS { get; set; }
        DateTime PROCESS_TIME { get; set; }
        String PROCESS_USER_NAME { get; set; }
    }
}
