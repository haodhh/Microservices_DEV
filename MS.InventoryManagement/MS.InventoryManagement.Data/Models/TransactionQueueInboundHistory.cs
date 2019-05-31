using System;

namespace MS.InventoryManagement.Data.Models
{
    /// <summary>
    /// TransactionQueueInboundHistory
    /// </summary>
    public class TransactionQueueInboundHistory
    {
        /// <summary>
        /// 
        /// </summary>
        public int TransactionQueueInboundHistoryId { get; set; }

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
        public bool ProcessedSuccessfully { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool DuplicateMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DateCreatedInbound { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DateCreated { get; set; }
    }
}