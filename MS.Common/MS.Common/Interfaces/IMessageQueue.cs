namespace MS.Common.Interfaces
{
    /// <summary>
    /// IMessageQueue
    /// </summary>
    public interface IMessageQueue
    {
        /// <summary>
        ///
        /// </summary>
        int TransactionQueueId { get; set; }

        /// <summary>
        ///
        /// </summary>
        string TransactionCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        string Payload { get; set; }

        /// <summary>
        ///
        /// </summary>
        string ExchangeName { get; set; }
    }
}