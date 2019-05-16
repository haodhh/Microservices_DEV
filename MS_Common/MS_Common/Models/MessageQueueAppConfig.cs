using System;

namespace MS_Common.Models
{
    /// <summary>
    /// MessageQueueAppConfig
    /// </summary>
    public class MessageQueueAppConfig
    {
        /// <summary>
        ///
        /// </summary>
        public string MessageQueueHostName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string MessageQueueUserName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string MessageQueuePassword { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string MessageQueueEnvironment { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ExchangeName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string RoutingKey { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string InboundMessageQueue { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string OutboundMessageQueues { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string LoggingExchangeName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string LoggingMessageQueue { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string OriginatingQueueName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Boolean SendToLoggingQueue { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string AcknowledgementMessageExchangeSuffix { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string AcknowledgementMessageQueueSuffix { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string TriggerExchangeName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string TriggerQueueName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Boolean QueueImmediately { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string InboundSemaphoreKey { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string OutboundSemaphoreKey { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int ProcessingIntervalSeconds { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int SendingIntervalSeconds { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int ReceivingIntervalSeconds { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string SignalRHubUrl { get; set; }
    }
}