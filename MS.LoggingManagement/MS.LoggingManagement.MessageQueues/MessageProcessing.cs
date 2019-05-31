using Microsoft.Extensions.Configuration;
using MS.Common.Interfaces;
using MS.Common.Models;
using MS.LoggingManagement.Data;
using MS.LoggingManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MS.LoggingManagement.MessageQueues
{
    public class MessageProcessing : IMessageQueueProcessing
    {
        private ILoggingManagementRepository _repository;

        public IConfiguration configuration { get; }

        private Boolean _sending = false;
        private readonly object _processingLock = new object();
        private readonly object _sendingLock = new object();

        /// <summary>
        /// Inventory Management Message Processing
        /// </summary>
        /// <param name="repository"></param>
        public MessageProcessing(ILoggingManagementRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Send Queue Messages
        /// </summary>
        /// <param name="messageQueueConfigurations"></param>
        /// <param name="outboundSemaphoreKey"></param>
        /// <param name="connectionStrings"></param>
        /// <returns></returns>
        public async Task<ResponseModel<List<MessageQueue>>> SendQueueMessages(List<IMessageQueueConfiguration> messageQueueConfigurations, string outboundSemaphoreKey, ConnectionStrings connectionStrings)

        {
            ResponseModel<List<MessageQueue>> returnResponse = new ResponseModel<List<MessageQueue>>();
            returnResponse.Entity = new List<MessageQueue>();

            lock (_sendingLock)
            {
                if (_sending)
                {
                    Console.WriteLine("Aborted iteration still sending");
                    return returnResponse;
                }

                _sending = true;
            }

            TransactionQueueSemaphore transactionQueueSemaphore = null;

            try
            {
                _repository.OpenConnection(connectionStrings.DefaultConnection);

                _repository.BeginTransaction((int)IsolationLevel.Serializable);

                transactionQueueSemaphore = await _repository.GetTransactionQueueSemaphore(outboundSemaphoreKey);
                if (transactionQueueSemaphore == null)
                {
                    transactionQueueSemaphore = new TransactionQueueSemaphore();
                    transactionQueueSemaphore.SemaphoreKey = outboundSemaphoreKey;
                    await _repository.CreateTransactionQueueSemaphore(transactionQueueSemaphore);
                }
                else
                {
                    await _repository.UpdateTransactionQueueSemaphore(transactionQueueSemaphore);
                }

                List<AcknowledgementsQueue> acknowledgementsQueue = await _repository.ProcessAcknowledgementsQueue();
                foreach (AcknowledgementsQueue transactionQueueItem in acknowledgementsQueue)
                {
                    MessageQueue message = new MessageQueue();
                    message.ExchangeName = transactionQueueItem.ExchangeName;
                    message.TransactionQueueId = transactionQueueItem.SenderTransactionQueueId;
                    message.TransactionCode = TransactionQueueTypes.Acknowledgement;
                    message.QueueName = transactionQueueItem.AcknowledgementQueue;

                    IMessageQueueConfiguration messageQueueConfiguration = messageQueueConfigurations.FirstOrDefault();
                    if (messageQueueConfiguration == null)
                    {
                        break;
                    }

                    ResponseModel<MessageQueue> messageQueueResponse = messageQueueConfiguration.SendAcknowledgementMessage(message);
                    if (messageQueueResponse.ReturnStatus == true)
                    {
                        await _repository.DeleteAcknowledgementsQueue(transactionQueueItem.AcknowledgementsQueueId);
                        returnResponse.Entity.Add(message);
                    }
                }

                await _repository.UpdateDatabase();

                _repository.CommitTransaction();

                _repository.CloseConnection();
            }
            catch (Exception ex)
            {
                _repository.RollbackTransaction();
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
            }
            finally
            {
                _repository.CloseConnection();
                _sending = false;
            }

            return returnResponse;
        }

        /// <summary>
        /// Commit Inbound Message
        /// </summary>
        /// <param name="messageQueue"></param>
        /// <returns></returns>
        public async Task<ResponseModel<MessageQueue>> CommitInboundMessage(MessageQueue messageQueue, ConnectionStrings connectionStrings)
        {
            ResponseModel<MessageQueue> returnResponse = new ResponseModel<MessageQueue>();

            try
            {
                _repository.OpenConnection(connectionStrings.DefaultConnection);
                _repository.BeginTransaction((int)IsolationLevel.ReadCommitted);

                MessagesSent existingMessageSent = await _repository.GetMessageSent(messageQueue.TransactionQueueId, messageQueue.ExchangeName, messageQueue.TransactionCode);

                if (messageQueue.QueueName != string.Empty && messageQueue.QueueName != null)
                {
                    MessagesReceived existingMessageReceived = await _repository.GetMessageReceived(messageQueue.TransactionQueueId, messageQueue.ExchangeName, messageQueue.TransactionCode, messageQueue.QueueName);
                    if (existingMessageReceived != null)
                    {
                        returnResponse.ReturnStatus = true;
                        return returnResponse;
                    }
                }

                if (existingMessageSent == null)
                {
                    MessagesSent messageSent = new MessagesSent();

                    messageSent.ExchangeName = messageQueue.ExchangeName;
                    messageSent.SenderTransactionQueueId = messageQueue.TransactionQueueId;
                    messageSent.TransactionCode = messageQueue.TransactionCode;
                    messageSent.Payload = messageQueue.Payload;

                    if (messageSent.TransactionCode == MessageQueueExchanges.ProductUpdated)
                    {
                        messageSent.AcknowledgementsRequired = MessageQueueFanouts.ProductUpdated;
                        messageSent.AcknowledgementsReceived = 0;
                    }
                    else if (messageSent.TransactionCode == MessageQueueExchanges.PurchaseOrderSubmitted)
                    {
                        messageSent.AcknowledgementsRequired = MessageQueueFanouts.PurchaseOrderSubmitted;
                        messageSent.AcknowledgementsReceived = 0;
                    }
                    else if (messageSent.TransactionCode == MessageQueueExchanges.SalesOrderSubmitted)
                    {
                        messageSent.AcknowledgementsRequired = MessageQueueFanouts.SalesOrderSubmitted;
                        messageSent.AcknowledgementsReceived = 0;
                    }
                    else if (messageSent.TransactionCode == MessageQueueExchanges.InventoryReceived)
                    {
                        messageSent.AcknowledgementsRequired = MessageQueueFanouts.InventoryReceived;
                        messageSent.AcknowledgementsReceived = 0;
                    }
                    else if (messageSent.TransactionCode == MessageQueueExchanges.InventoryShipped)
                    {
                        messageSent.AcknowledgementsRequired = MessageQueueFanouts.InventoryShipped;
                        messageSent.AcknowledgementsReceived = 0;
                    }

                    if (messageQueue.QueueName != string.Empty && messageQueue.QueueName != null)
                    {
                        existingMessageSent.AcknowledgementsReceived = existingMessageSent.AcknowledgementsReceived + 1;
                    }

                    await _repository.CreateMessagesSent(messageSent);
                }

                if (messageQueue.QueueName != string.Empty && messageQueue.QueueName != null)
                {
                    if (existingMessageSent != null)
                    {
                        existingMessageSent.AcknowledgementsReceived = existingMessageSent.AcknowledgementsReceived + 1;
                        await _repository.UpdateMessagesSent(existingMessageSent);
                    }

                    MessagesReceived messageReceived = new MessagesReceived();
                    messageReceived.ExchangeName = messageQueue.ExchangeName;
                    messageReceived.SenderTransactionQueueId = messageQueue.TransactionQueueId;
                    messageReceived.TransactionCode = messageQueue.TransactionCode;
                    messageReceived.Payload = messageQueue.Payload;
                    messageReceived.QueueName = messageQueue.QueueName;

                    await _repository.CreateMessagesReceived(messageReceived);

                    if (existingMessageSent.AcknowledgementsReceived == existingMessageSent.AcknowledgementsRequired)
                    {
                        AcknowledgementsQueue acknowledgementsQueue = new AcknowledgementsQueue();
                        acknowledgementsQueue.ExchangeName = messageQueue.ExchangeName;
                        acknowledgementsQueue.SenderTransactionQueueId = messageQueue.TransactionQueueId;
                        acknowledgementsQueue.TransactionCode = messageQueue.TransactionCode;

                        if (acknowledgementsQueue.TransactionCode == MessageQueueExchanges.ProductUpdated)
                        {
                            acknowledgementsQueue.AcknowledgementQueue = MessageQueueEndpoints.InventoryQueue;
                        }
                        else if (acknowledgementsQueue.TransactionCode == MessageQueueExchanges.PurchaseOrderSubmitted)
                        {
                            acknowledgementsQueue.AcknowledgementQueue = MessageQueueEndpoints.PurchaseOrderQueue;
                        }
                        else if (acknowledgementsQueue.TransactionCode == MessageQueueExchanges.SalesOrderSubmitted)
                        {
                            acknowledgementsQueue.AcknowledgementQueue = MessageQueueEndpoints.SalesOrderQueue;
                        }
                        else if (acknowledgementsQueue.TransactionCode == MessageQueueExchanges.InventoryReceived)
                        {
                            acknowledgementsQueue.AcknowledgementQueue = MessageQueueEndpoints.InventoryQueue;
                        }
                        else if (acknowledgementsQueue.TransactionCode == MessageQueueExchanges.InventoryShipped)
                        {
                            acknowledgementsQueue.AcknowledgementQueue = MessageQueueEndpoints.InventoryQueue;
                        }

                        await _repository.CreateAcknowledgementsQueue(acknowledgementsQueue);
                    }
                }

                await _repository.UpdateDatabase();

                _repository.CommitTransaction();

                returnResponse.ReturnStatus = true;
            }
            catch (Exception ex)
            {
                _repository.RollbackTransaction();
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
            }
            finally
            {
                _repository.CloseConnection();
            }

            returnResponse.Entity = messageQueue;

            return returnResponse;
        }

        /// <summary>
        /// Process Messages
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseModel<List<MessageQueue>>> ProcessMessages(string inboundSemaphoreKey, ConnectionStrings connectionStrings)
        {
            await Task.Delay(0);
            ResponseModel<List<MessageQueue>> returnResponse = new ResponseModel<List<MessageQueue>>();
            returnResponse.Entity = new List<MessageQueue>();
            return returnResponse;
        }
    }
}