using System;

namespace MS.SalesOrderManagement.Data.Models
{
    public class TransactionQueueOutboundHistory
    {
        public int TransactionQueueOutboundHistoryId { get; set; }
        public int TransactionQueueOutboundId { get; set; }
        public string TransactionCode { get; set; }
        public string Payload { get; set; }
        public string ExchangeName { get; set; }
        public Boolean SentToExchange { get; set; }
        public DateTime DateOutboundTransactionCreated { get; set; }
        public DateTime DateSentToExchange { get; set; }
        public DateTime DateToResendToExchange { get; set; }
        public DateTime DateCreated { get; set; }
    }
}