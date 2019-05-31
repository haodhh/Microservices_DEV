using System;

namespace MS.PurchaseOrderManagement.Data.Models
{
    public class TransactionQueueInbound
    {
        public int TransactionQueueInboundId { get; set; }
        public int SenderTransactionQueueId { get; set; }
        public string TransactionCode { get; set; }
        public string Payload { get; set; }
        public string ExchangeName { get; set; }
        public DateTime DateCreated { get; set; }
    }
}