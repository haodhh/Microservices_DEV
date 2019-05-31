using MS.Common.Interfaces;
using MS.LoggingManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MS.LoggingManagement.Data
{
    public interface ILoggingManagementRepository : IDataRepository, IDisposable
    {
        Task CreateMessagesSent(MessagesSent messagesSent);

        Task CreateAcknowledgementsQueue(AcknowledgementsQueue acknowledgementsQueue);

        Task CreateMessagesReceived(MessagesReceived messagesReceived);

        Task<MessagesSent> GetMessageSent(int senderId, string exchangeName, string transactionCode);

        Task<MessagesReceived> GetMessageReceived(int senderId, string exchangeName, string transactionCode, string queueName);

        Task UpdateMessagesSent(MessagesSent messagesSent);

        Task<List<AcknowledgementsQueue>> ProcessAcknowledgementsQueue();

        Task DeleteAcknowledgementsQueue(int acknowledgementsQueueId);

        Task<TransactionQueueSemaphore> GetTransactionQueueSemaphore(string semaphoreKey);

        Task UpdateTransactionQueueSemaphore(TransactionQueueSemaphore transactionQueueSemaphore);

        Task CreateTransactionQueueSemaphore(TransactionQueueSemaphore transactionQueueSemaphore);
    }
}