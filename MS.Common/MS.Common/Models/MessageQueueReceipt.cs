namespace MS.Common.Models
{
    /// <summary>
    /// MessageQueueReceipt
    /// </summary>
    public class MessageQueueReceipt
    {
        /// <summary>
        ///
        /// </summary>
        public string TransactionCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int TransactionQueueId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int MessageQueueDirection { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Payload { get; set; }
    }
}