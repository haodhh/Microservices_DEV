using MS_Common.Models;
using RabbitMQ.Client.MessagePatterns;

namespace MS_Common.Interfaces
{
    /// <summary>
    /// IMessageQueueConfiguration
    /// </summary>
    public interface IMessageQueueConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetOriginatingQueueName();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Subscription GetSubscription();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        void AddQueue(string queueName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        void InitializeInboundMessageQueueing(string queueName);

        /// <summary>
        /// 
        /// </summary>
        void InitializeOutboundMessageQueueing();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggingExchangeName"></param>
        /// <param name="loggingQueueName"></param>
        void InitializeLoggingExchange(string loggingExchangeName, string loggingQueueName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ResponseModel<MessageQueue> SendMessage(MessageQueue entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ResponseModel<MessageQueue> SendAcknowledgementMessage(MessageQueue entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageQueue"></param>
        /// <param name="loggingExchangeName"></param>
        /// <returns></returns>
        ResponseModel<MessageQueue> SendReceivedMessageToLoggingQueue(MessageQueue messageQueue, string loggingExchangeName);

        /// <summary>
        /// 
        /// </summary>
        string TransactionCode { get; set; }
    }
}