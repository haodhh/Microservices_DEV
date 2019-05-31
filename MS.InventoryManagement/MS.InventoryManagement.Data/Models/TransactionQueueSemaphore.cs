using System;

namespace MS.InventoryManagement.Data.Models
{
    /// <summary>
    /// TransactionQueueSemaphore
    /// </summary>
    public class TransactionQueueSemaphore
    {
        /// <summary>
        /// 
        /// </summary>
        public int TransactionQueueSemaphoreId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SemaphoreKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DateUpdated { get; set; }
    }
}