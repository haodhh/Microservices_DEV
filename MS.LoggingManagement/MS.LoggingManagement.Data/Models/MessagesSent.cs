﻿using System;

namespace MS.LoggingManagement.Data.Models
{
    public class MessagesSent
    {
        public int MessagesSentId { get; set; }
        public int SenderTransactionQueueId { get; set; }
        public string TransactionCode { get; set; }
        public string ExchangeName { get; set; }
        public string Payload { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int AcknowledgementsRequired { get; set; }
        public int AcknowledgementsReceived { get; set; }
    }
}