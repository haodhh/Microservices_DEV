using MS.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.LoggingManagement.MessageQueues
{
    public static class LogAppConfig
    {
        public static MessageQueueAppConfig CreateMessageQueueAppConfig()
        {
            var model = new MessageQueueAppConfig();

            model.MessageQueueHostName = "localhost";
            model.MessageQueueUserName = "guest";
            model.MessageQueuePassword = "guest";
            model.MessageQueueEnvironment = "DEV";
            model.SignalRHubUrl = "";
            model.RoutingKey = "ERP-DEV";
            model.InboundSemaphoreKey = "InventoryManagementInbound";
            model.OutboundSemaphoreKey = "InventoryManagementOutbound";
            model.AcknowledgementMessageExchangeSuffix = "ACK";
            model.SendToLoggingQueue = false;
            model.ProcessingIntervalSeconds = 60;
            model.SendingIntervalSeconds = 60;
            model.ReceivingIntervalSeconds = 900;
            model.QueueImmediately = false;
            model.TriggerExchangeName = "LoggingManagement_TRG_DEV";
            model.TriggerQueueName = "LoggingManagement_DEV";

            return model;
        }

        public static ConnectionStrings CreateConnectionStrings()
        {
            var model = new ConnectionStrings();

            model.DefaultConnection = "Data Source=.\\SQLEXPRESS;Database=MS_LoggingManagement_DEV;Trusted_Connection=True";

            return model;
        }
    }
}
