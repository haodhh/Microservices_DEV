
using Microsoft.Extensions.Configuration;
using MS.Common.Interfaces;
using MS.Common.Models;
using MS.Common.Models.MessageQueuePayloads;
using MS.PurchaseOrderManagement.Data;
using MS.PurchaseOrderManagement.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MS.PurchaseOrderManagement.MessageQueues
{
    public class MessageProcessing : IMessageQueueProcessing
    {
        private IPurchaseOrderManagementRepository _repository;

        public IConfiguration configuration { get; }

        private Boolean _sending = false;
        private Boolean _processing = false;
        private readonly object _processingLock = new object();
        private readonly object _sendingLock = new object();

        /// <summary>
        /// PurchaseOrder Management Message Processing
        /// </summary>
        /// <param name="purchaseOrderManagementDataService"></param>
        public MessageProcessing(IPurchaseOrderManagementRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Send Queue Messages
        /// </summary>
        /// <param name="messageQueueing"></param>
        /// <param name="outboundSemaphoreKey"></param>
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

                List<TransactionQueueOutbound> transactionQueue = await _repository.GetOutboundTransactionQueue();
                foreach (TransactionQueueOutbound transactionQueueItem in transactionQueue)
                {
                    MessageQueue message = new MessageQueue();
                    message.ExchangeName = transactionQueueItem.ExchangeName;
                    message.TransactionQueueId = transactionQueueItem.TransactionQueueOutboundId;
                    message.TransactionCode = transactionQueueItem.TransactionCode;
                    message.Payload = transactionQueueItem.Payload;

                    IMessageQueueConfiguration messageQueueConfiguration = messageQueueConfigurations.Where(x => x.TransactionCode == message.TransactionCode).FirstOrDefault();
                    if (messageQueueConfiguration == null)
                    {
                        break;
                    }

                    ResponseModel<MessageQueue> messageQueueResponse = messageQueueConfiguration.SendMessage(message);

                    if (messageQueueResponse.ReturnStatus == true)
                    {
                        transactionQueueItem.SentToExchange = true;
                        transactionQueueItem.DateSentToExchange = DateTime.UtcNow;
                        await _repository.UpdateOutboundTransactionQueue(transactionQueueItem);

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

                TransactionQueueInbound transactionQueue = new TransactionQueueInbound();
                transactionQueue.ExchangeName = messageQueue.ExchangeName;
                transactionQueue.SenderTransactionQueueId = messageQueue.TransactionQueueId;
                transactionQueue.TransactionCode = messageQueue.TransactionCode;
                transactionQueue.Payload = messageQueue.Payload;

                await _repository.CreateInboundTransactionQueue(transactionQueue);

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
            ResponseModel<List<MessageQueue>> returnResponse = new ResponseModel<List<MessageQueue>>();
            returnResponse.Entity = new List<MessageQueue>();

            TransactionQueueSemaphore transactionQueueSemaphore = null;

            lock (_processingLock)
            {
                if (_processing == true)
                {
                    Console.WriteLine("Processing iteration aborted");
                    return returnResponse;
                }

                _processing = true;
            }

            try
            {
                _repository.OpenConnection(connectionStrings.DefaultConnection);
                _repository.BeginTransaction((int)IsolationLevel.Serializable);

                transactionQueueSemaphore = await _repository.GetTransactionQueueSemaphore(inboundSemaphoreKey);
                if (transactionQueueSemaphore == null)
                {
                    transactionQueueSemaphore = new TransactionQueueSemaphore();
                    transactionQueueSemaphore.SemaphoreKey = inboundSemaphoreKey;
                    await _repository.CreateTransactionQueueSemaphore(transactionQueueSemaphore);
                }
                else
                {
                    await _repository.UpdateTransactionQueueSemaphore(transactionQueueSemaphore);
                }

                List<TransactionQueueInbound> transactionQueue = await _repository.GetInboundTransactionQueue();
                foreach (TransactionQueueInbound transactionQueueItem in transactionQueue)
                {
                    int senderId = transactionQueueItem.SenderTransactionQueueId;
                    string exchangeName = transactionQueueItem.ExchangeName;
                    string transactionCode = transactionQueueItem.TransactionCode;

                    TransactionQueueInboundHistory transactionHistory = await _repository.GetInboundTransactionQueueHistoryBySender(senderId, exchangeName);
                    if (transactionHistory != null)
                    {
                        await LogDuplicateMessage(transactionQueueItem);
                        await _repository.DeleteInboundTransactionQueueEntry(transactionQueueItem.TransactionQueueInboundId);
                    }
                    else if (transactionCode == TransactionQueueTypes.ProductUpdated)
                    {
                        await ProductUpdated(transactionQueueItem);
                        await _repository.DeleteInboundTransactionQueueEntry(transactionQueueItem.TransactionQueueInboundId);
                    }
                    else if (transactionCode == TransactionQueueTypes.InventoryReceived)
                    {
                        await InventoryReceived(transactionQueueItem);
                        await _repository.DeleteInboundTransactionQueueEntry(transactionQueueItem.TransactionQueueInboundId);
                    }
                    else if (transactionCode == TransactionQueueTypes.Acknowledgement)
                    {
                        await ProcessAcknowledgement(transactionQueueItem);
                        await _repository.DeleteInboundTransactionQueueEntry(transactionQueueItem.TransactionQueueInboundId);
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
                _processing = false;
                _repository.CloseConnection();
            }

            return returnResponse;
        }

        /// <summary>
        /// Product Updated
        /// </summary>
        /// <param name="transaction"></param>
        private async Task ProductUpdated(TransactionQueueInbound transaction)
        {
            ProductUpdatePayload payload = JsonConvert.DeserializeObject<ProductUpdatePayload>(transaction.Payload);

            int productMasterId = payload.ProductId;

            Product product = await _repository.GetProductInformationByProductMasterForUpdate(productMasterId);
            if (product != null)
            {
                product.ProductNumber = payload.ProductNumber;
                product.Description = payload.Description;
                product.UnitPrice = payload.UnitPrice;

                await _repository.UpdateProduct(product);
            }
            else
            {
                product = new Product();
                product.AccountId = payload.AccountId;
                product.ProductNumber = payload.ProductNumber;
                product.ProductMasterId = payload.ProductId;
                product.Description = payload.Description;
                product.UnitPrice = payload.UnitPrice;

                await _repository.CreateProduct(product);
            }

            await LogSuccessfullyProcessed(transaction);
        }

        /// <summary>
        /// Inventory Received
        /// </summary>
        /// <param name="transaction"></param>
        private async Task InventoryReceived(TransactionQueueInbound transaction)
        {
            InventoryTransactionPayload payload = JsonConvert.DeserializeObject<InventoryTransactionPayload>(transaction.Payload);

            int purchaseOrderDetailId = payload.MasterEntityId;

            PurchaseOrderDetail purchaseOrderDetail = await _repository.GetPurchaseOrderDetailForUpdate(purchaseOrderDetailId);
            if (purchaseOrderDetail != null)
            {
                purchaseOrderDetail.ReceiviedQuantity = purchaseOrderDetail.ReceiviedQuantity + payload.Quantity;

                await _repository.UpdatePurchaseOrderDetail(purchaseOrderDetail);
            }

            await LogSuccessfullyProcessed(transaction);
        }

        /// <summary>
        /// Log Successfully Processed
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        private async Task LogSuccessfullyProcessed(TransactionQueueInbound transaction)
        {
            TransactionQueueInboundHistory transactionHistory = new TransactionQueueInboundHistory();
            transactionHistory.TransactionQueueInboundId = transaction.TransactionQueueInboundId;
            transactionHistory.SenderTransactionQueueId = transaction.SenderTransactionQueueId;
            transactionHistory.TransactionCode = transaction.TransactionCode;
            transactionHistory.Payload = transaction.Payload;
            transactionHistory.ExchangeName = transaction.ExchangeName;
            transactionHistory.ProcessedSuccessfully = true;
            transactionHistory.DuplicateMessage = false;
            transactionHistory.ErrorMessage = string.Empty;
            transactionHistory.DateCreatedInbound = transaction.DateCreated;

            await _repository.CreateInboundTransactionQueueHistory(transactionHistory);
        }

        /// <summary>
        ///  Log Duplicate Message
        /// </summary>
        /// <param name="transactionQueueItem"></param>
        /// <returns></returns>
        private async Task LogDuplicateMessage(TransactionQueueInbound transactionQueueItem)
        {
            // log history as duplicate
            TransactionQueueInboundHistory transactionHistory = new TransactionQueueInboundHistory();
            transactionHistory.TransactionQueueInboundId = transactionQueueItem.TransactionQueueInboundId;
            transactionHistory.SenderTransactionQueueId = transactionQueueItem.SenderTransactionQueueId;
            transactionHistory.TransactionCode = transactionQueueItem.TransactionCode;
            transactionHistory.Payload = transactionQueueItem.Payload;
            transactionHistory.ExchangeName = transactionQueueItem.ExchangeName;
            transactionHistory.ProcessedSuccessfully = false;
            transactionHistory.DuplicateMessage = true;
            transactionHistory.ErrorMessage = string.Empty;
            transactionHistory.DateCreatedInbound = transactionQueueItem.DateCreated;

            await _repository.CreateInboundTransactionQueueHistory(transactionHistory);
        }

        /// <summary>
        /// Process Acknowledgement
        /// </summary>
        /// <param name="transaction"></param>
        private async Task ProcessAcknowledgement(TransactionQueueInbound transaction)
        {
            int transactionId = transaction.SenderTransactionQueueId;

            TransactionQueueOutbound transactionQueueItem = await _repository.GetOutboundTransactionQueueItemById(transactionId);
            if (transactionQueueItem != null)
            {
                await LogOutboundTransactionToHistory(transactionQueueItem);
            }
        }

        /// <summary>
        /// Log Outbound Transaction To History
        /// </summary>
        /// <param name="transactionQueueItem"></param>
        /// <returns></returns>
        private async Task LogOutboundTransactionToHistory(TransactionQueueOutbound transactionQueueItem)
        {
            TransactionQueueOutboundHistory transactionHistory = new TransactionQueueOutboundHistory();
            transactionHistory.TransactionQueueOutboundId = transactionQueueItem.TransactionQueueOutboundId;
            transactionHistory.TransactionCode = transactionQueueItem.TransactionCode;
            transactionHistory.Payload = transactionQueueItem.Payload;
            transactionHistory.ExchangeName = transactionQueueItem.ExchangeName;
            transactionHistory.SentToExchange = transactionQueueItem.SentToExchange;
            transactionHistory.DateOutboundTransactionCreated = transactionQueueItem.DateCreated;
            transactionHistory.DateSentToExchange = transactionQueueItem.DateSentToExchange;
            transactionHistory.DateToResendToExchange = transactionQueueItem.DateToResendToExchange;

            await _repository.CreateOutboundTransactionQueueHistory(transactionHistory);
            await _repository.DeleteOutboundTransactionQueueEntry(transactionQueueItem.TransactionQueueOutboundId);
        }
    }
}