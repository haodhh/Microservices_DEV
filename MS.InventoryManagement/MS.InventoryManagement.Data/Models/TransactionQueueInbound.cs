using System;

namespace MS.InventoryManagement.Data.Models
{
    /// <summary>
    /// TransactionQueueInbound
    /// </summary>
    public class TransactionQueueInbound
    {
        /// <summary>
        /// 
        /// </summary>
        public int TransactionQueueInboundId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SenderTransactionQueueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TransactionCode { get; set; }

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
        public DateTime DateCreated { get; set; }
    }
}