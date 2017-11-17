using ArchPM.Messaging.Infrastructure.EventArg;

namespace ArchPM.Messaging.Infrastructure
{
    /// <summary>
    /// An abstract class to handle business
    /// </summary>
    public abstract class MessageReceivedCommandHandler
    {
        /// <summary>
        /// Executes the given business. Takes read message to be able to operate
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract MessageReceivedBusinessHandlerResultTypes Execute(MessageReceivedEventArgs args);
    }

}
