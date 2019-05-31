using System;

namespace MS.Common.Models
{
    /// <summary>
    /// MessageQueue
    /// </summary>
    public class MessageQueue
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
        public string Payload { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ExchangeName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Guid MessageGuid { get; set; }
    }
}