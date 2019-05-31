using System;

namespace MS.InventoryManagement.Data.Models
{
    /// <summary>
    /// TransactionQueueOutboundHistory
    /// </summary>
    public class TransactionQueueOutboundHistory
    {
        /// <summary>
        /// 
        /// </summary>
        public int TransactionQueueOutboundHistoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TransactionQueueOutboundId { get; set; }

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
        public Boolean SentToExchange { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DateOutboundTransactionCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DateSentToExchange { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DateToResendToExchange { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DateCreated { get; set; }
    }
}