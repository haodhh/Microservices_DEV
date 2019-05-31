using System;

namespace MS.LoggingManagement.Data.Models
{
    public class TransactionQueueSemaphore
    {
        public int TransactionQueueSemaphoreId { get; set; }
        public string SemaphoreKey { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}