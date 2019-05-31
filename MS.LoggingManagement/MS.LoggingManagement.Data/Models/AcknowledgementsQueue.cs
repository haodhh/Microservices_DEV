using System;

namespace MS.LoggingManagement.Data.Models
{
    public class AcknowledgementsQueue
    {
        public int AcknowledgementsQueueId { get; set; }
        public int SenderTransactionQueueId { get; set; }
        public string TransactionCode { get; set; }
        public string ExchangeName { get; set; }
        public string AcknowledgementQueue { get; set; }
        public DateTime DateCreated { get; set; }
    }
}