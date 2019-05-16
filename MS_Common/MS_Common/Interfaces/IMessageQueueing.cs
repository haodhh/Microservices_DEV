using MS_Common.Models;
using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace MS_Common.Interfaces
{
    /// <summary>
    /// IMessageQueueing
    /// </summary>
    public interface IMessageQueueing
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionStrings"></param>
        void SetConnectionStrings(ConnectionStrings connectionStrings);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        void InitializeMessageQueueing(string hostName, string userName, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ResponseModel<MessageQueue> SendMessage(object entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="subject"></param>
        /// <param name="_messageProcessor"></param>
        /// <returns></returns>
        Task ReceiveMessages(string queueName, Subject<MessageQueue> subject, IMessageQueueProcessing _messageProcessor);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageGuid"></param>
        void SendAcknowledgement(Guid messageGuid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageQueueAppConfig"></param>
        /// <returns></returns>
        ResponseModel<MessageQueue> BroadcastTransaction(MessageQueueAppConfig messageQueueAppConfig);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outboundSemaphoreKey"></param>
        void SetOutboundSemaphoreKey(string outboundSemaphoreKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inboundSemaphoreKey"></param>
        void SetInboundSemaphoreKey(string inboundSemaphoreKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="routingKey"></param>
        void InitializeExchange(string exchangeName, string routingKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="routingKey"></param>
        void InitializeLoggingExchange(string exchangeName, string routingKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        void InitializeQueue(string queueName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="routingKey"></param>
        void InitializeQueue(string queueName, string routingKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originatingQueueName"></param>
        /// <param name="loggingQueueName"></param>
        /// <param name="sendToLoggingQueue"></param>
        void InitializeLogging(string originatingQueueName, string loggingQueueName, Boolean sendToLoggingQueue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageQueue"></param>
        /// <returns></returns>
        ResponseModel<MessageQueue> SendReceivedMessageToLoggingQueue(MessageQueue messageQueue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageQueue"></param>
        /// <returns></returns>
        ResponseModel<MessageQueue> SendAcknowledgementMessage(MessageQueue messageQueue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="acknowledgementMessageExchangeSuffix"></param>
        /// <param name="acknowledgementMessageQueueSuffix"></param>
        void InitializeAcknowledgementConfiguration(string acknowledgementMessageExchangeSuffix, string acknowledgementMessageQueueSuffix);
    }
}