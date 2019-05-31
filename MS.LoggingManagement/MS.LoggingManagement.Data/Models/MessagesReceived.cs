using System;

namespace MS.LoggingManagement.Data.Models
{
    public class MessagesReceived
    {
        public int MessagesReceivedId { get; set; }
        public int SenderTransactionQueueId { get; set; }
        public string QueueName { get; set; }
        public string TransactionCode { get; set; }
        public string ExchangeName { get; set; }
        public string Payload { get; set; }
        public DateTime DateCreated { get; set; }
    }
}