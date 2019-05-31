using System;

namespace MS.InventoryManagement.Data.Models
{
    /// <summary>
    /// TransactionQueueOutbound
    /// </summary>
    public class TransactionQueueOutbound
    {
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
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DateSentToExchange { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DateToResendToExchange { get; set; }
    }
}