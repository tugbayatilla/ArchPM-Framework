using ArchPM.Messaging.Infrastructure.EventArg;

namespace ArchPM.Messaging.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Messaging.Infrastructure.MessageReceivedCommandHandler" />
    public class DefaultMessageReceivedCommandHandler : MessageReceivedCommandHandler
    {
        /// <summary>
        /// Executes the given business. Takes read message to be able to operate
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Always returns Success</returns>
        public override MessageReceivedBusinessHandlerResultTypes Execute(MessageReceivedEventArgs args)
        {
            return MessageReceivedBusinessHandlerResultTypes.Success;
        }


    }
}
