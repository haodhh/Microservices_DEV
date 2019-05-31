using MS.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.InventoryManagement.MessageQueues
{
    public static class InventoryAppConfig
    {
        public static MessageQueueAppConfig CreateMessageQueueAppConfig()
        {
            var model = new MessageQueueAppConfig();

            model.MessageQueueHostName = "localhost";
            model.MessageQueueUserName = "guest";
            model.MessageQueuePassword = "guest";
            model.MessageQueueEnvironment = "DEV";
            model.SignalRHubUrl = "https://localhost:44302/MessageQueueHub";
            model.RoutingKey = "ERP-DEV";
            model.InboundSemaphoreKey = "InventoryManagementInbound";
            model.OutboundSemaphoreKey = "InventoryManagementOutbound";
            model.AcknowledgementMessageExchangeSuffix = "_ACK_DEV";
            model.AcknowledgementMessageQueueSuffix = "_DEV";
            model.SendToLoggingQueue = true;
            model.ProcessingIntervalSeconds = 900;
            model.SendingIntervalSeconds = 900;
            model.ReceivingIntervalSeconds = 900;
            model.ExchangeName = "InventoryManagement_DEV";
            model.InboundMessageQueue = "InventoryManagement_DEV";
            model.OutboundMessageQueues = "SalesOrderManagement_DEV,LoggingManagement_DEV";
            model.LoggingExchangeName = "LoggingManagement_DEV";
            model.LoggingMessageQueue = "LoggingManagement_DEV";
            model.QueueImmediately = true;
            model.TriggerExchangeName = "InventoryManagement_TRG_DEV";
            model.TriggerQueueName = "InventoryManagement_DEV";

            return model;
        }

        public static ConnectionStrings CreateConnectionStrings()
        {
            var model = new ConnectionStrings();

            model.DefaultConnection = "Data Source=.\\SQLEXPRESS;Database=MS_InventoryManagement_DEV;Trusted_Connection=True";

            return model;
        }
    }
}
