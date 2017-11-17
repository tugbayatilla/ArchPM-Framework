
namespace ArchPM.Messaging.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public enum MessageReceivedBusinessHandlerResultTypes
    {
        /// <summary>
        /// Operation successfull and remove message
        /// </summary>
        Success,
        /// <summary>
        /// Operation failed and resend message again
        /// </summary>
        Retry,
        /// <summary>
        /// Operation failed, remove all related messages
        /// </summary>
        DeleteAllRelated,
        /// <summary>
        /// Operation successfull, but remove message
        /// </summary>
        RemoveMessage
    }

}
