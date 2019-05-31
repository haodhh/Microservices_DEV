using MS.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.SalesOrderManagement.MessageQueues
{
    public static class SaleAppConfig
    {
        public static MessageQueueAppConfig CreateMessageQueueAppConfig()
        {
            var model = new MessageQueueAppConfig();

            model.MessageQueueHostName = "localhost";
            model.MessageQueueUserName = "guest";
            model.MessageQueuePassword = "guest";
            model.MessageQueueEnvironment = "DEV";
            model.SignalRHubUrl = "https://localhost:44304/MessageQueueHub";
            model.RoutingKey = "ERP-DEV";
            model.InboundSemaphoreKey = "SalesOrderManagementInbound";
            model.OutboundSemaphoreKey = "SalesOrderManagementOutbound";
            model.AcknowledgementMessageExchangeSuffix = "_ACK_DEV";
            model.AcknowledgementMessageQueueSuffix = "_DEV";
            model.SendToLoggingQueue = true;
            model.ProcessingIntervalSeconds = 900;
            model.SendingIntervalSeconds = 900;
            model.ReceivingIntervalSeconds = 900;
            model.ExchangeName = "SalesOrderManagement_DEV";
            model.InboundMessageQueue = "SalesOrderManagement_DEV";
            model.OutboundMessageQueues = "SalesOrderManagement_DEV,LoggingManagement_DEV";
            model.LoggingExchangeName = "LoggingManagement_DEV";
            model.LoggingMessageQueue = "LoggingManagement_DEV";
            model.QueueImmediately = true;
            model.TriggerExchangeName = "SalesOrderManagement_TRG_DEV";
            model.TriggerQueueName = "SalesOrderManagement_DEV";

            return model;
        }

        public static ConnectionStrings CreateConnectionStrings()
        {
            var model = new ConnectionStrings();

            model.DefaultConnection = "Data Source=.\\SQLEXPRESS;Database=MS_SalesOrderManagement_DEV;Trusted_Connection=True";

            return model;
        }
    }
}
