using MS.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.PurchaseOrderManagement.MessageQueues
{
    public static class PurchaseAppConfig
    {
        public static MessageQueueAppConfig CreateMessageQueueAppConfig()
        {
            var model = new MessageQueueAppConfig();

            model.MessageQueueHostName = "localhost";
            model.MessageQueueUserName = "guest";
            model.MessageQueuePassword = "guest";
            model.MessageQueueEnvironment = "DEV";
            model.SignalRHubUrl = "https://localhost:44303/MessageQueueHub";
            model.RoutingKey = "ERP-DEV";
            model.InboundSemaphoreKey = "PurchaseOrderManagementInbound";
            model.OutboundSemaphoreKey = "PurchaseOrderManagementOutbound";
            model.AcknowledgementMessageExchangeSuffix = "_ACK_DEV";
            model.AcknowledgementMessageQueueSuffix = "_DEV";
            model.SendToLoggingQueue = true;
            model.ProcessingIntervalSeconds = 900;
            model.SendingIntervalSeconds = 900;
            model.ReceivingIntervalSeconds = 900;
            model.ExchangeName = "PurchaseOrderManagement_DEV";
            model.InboundMessageQueue = "PurchaseOrderManagement_DEV";
            model.LoggingExchangeName = "LoggingManagement_DEV";
            model.LoggingMessageQueue = "LoggingManagement_DEV";
            model.QueueImmediately = true;
            model.TriggerExchangeName = "PurchaseOrderManagement_TRG_DEV";
            model.TriggerQueueName = "PurchaseManagement_DEV";

            return model;
        }

        public static ConnectionStrings CreateConnectionStrings()
        {
            var model = new ConnectionStrings();

            model.DefaultConnection = "Data Source=.\\SQLEXPRESS;Database=MS_PurchaseOrderManagement_DEV;Trusted_Connection=True";

            return model;
        }
    }
}
