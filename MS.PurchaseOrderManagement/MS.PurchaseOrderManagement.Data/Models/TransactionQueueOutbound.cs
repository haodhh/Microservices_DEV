using System;

namespace MS.PurchaseOrderManagement.Data.Models
{
    public class TransactionQueueOutbound
    {
        public int TransactionQueueOutboundId { get; set; }
        public string TransactionCode { get; set; }
        public string Payload { get; set; }
        public string ExchangeName { get; set; }
        public Boolean SentToExchange { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateSentToExchange { get; set; }
        public DateTime DateToResendToExchange { get; set; }
    }
}