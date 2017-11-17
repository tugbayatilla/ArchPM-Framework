
namespace ArchPM.Messaging
{
    /// <summary>
    /// 
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        ///  Operation will not be transactional.
        /// </summary>
        None = 0,

        /// <summary>
        /// A transaction type used for Microsoft Transaction Server (MTS) or COM+ 1.0
        //     Services. If there is already an MTS transaction context, it will be used
        //     when sending or receiving the message.
        /// </summary>
        Automatic = 1,

        /// <summary>
        /// A transaction type used for single internal transactions.
        /// </summary>
        Single = 3,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum UsingTransaction
    {
        /// <summary>
        /// Creates Transaction Queue at localmachine and use send/receive operation with MessageQueueTransaction
        /// </summary>
        UseInternalTransaction,
        /// <summary>
        /// Creates Transaction Queue at localmachine and use send/receive operation as MessageQueueTransactionType as Automatic
        /// </summary>
        UseExternalTransaction,
        /// <summary>
        /// Creates Non-Transactional Queue at localmachine and send/receive operation with given TransactionType. Default TransactionType is None
        /// </summary>
        NoTransactionAtAll
    }
}
