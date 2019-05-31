using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MS.Common.Interfaces;
using MS.Common.MessageQueues;
using MS.Common.Models;
using MS.InventoryManagement.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MS.InventoryManagement.MessageQueues
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            //
            //	get configuration information
            //
            MessageQueueAppConfig messageQueueAppConfig = InventoryAppConfig.CreateMessageQueueAppConfig();
            ConnectionStrings connectionStrings = InventoryAppConfig.CreateConnectionStrings();

            //
            //	set up sending queue
            //
            IMessageQueueConnection sendingQueueConnection = new MessageQueueConnection(messageQueueAppConfig);
            sendingQueueConnection.CreateConnection();

            List<IMessageQueueConfiguration> messageQueueConfigurations = new List<IMessageQueueConfiguration>();
            //
            //	Inventory Received Transactions
            //
            IMessageQueueConfiguration inventoryReceivedConfiguration = new MessageQueueConfiguration(MessageQueueExchanges.InventoryReceived, messageQueueAppConfig, sendingQueueConnection);

            inventoryReceivedConfiguration.AddQueue(MessageQueueEndpoints.SalesOrderQueue);
            inventoryReceivedConfiguration.AddQueue(MessageQueueEndpoints.PurchaseOrderQueue);
            inventoryReceivedConfiguration.AddQueue(MessageQueueEndpoints.LoggingQueue);

            inventoryReceivedConfiguration.InitializeOutboundMessageQueueing();
            messageQueueConfigurations.Add(inventoryReceivedConfiguration);
            //
            //	Product Creation and Updates
            //
            IMessageQueueConfiguration productUpdatedConfiguration = new MessageQueueConfiguration(MessageQueueExchanges.ProductUpdated, messageQueueAppConfig, sendingQueueConnection);

            productUpdatedConfiguration.AddQueue(MessageQueueEndpoints.SalesOrderQueue);
            productUpdatedConfiguration.AddQueue(MessageQueueEndpoints.PurchaseOrderQueue);
            productUpdatedConfiguration.AddQueue(MessageQueueEndpoints.LoggingQueue);

            productUpdatedConfiguration.InitializeOutboundMessageQueueing();
            messageQueueConfigurations.Add(productUpdatedConfiguration);
            //
            //	Inventory Shipped Transactions
            //
            IMessageQueueConfiguration inventoryShippedConfiguration = new MessageQueueConfiguration(MessageQueueExchanges.InventoryShipped, messageQueueAppConfig, sendingQueueConnection);

            inventoryShippedConfiguration.AddQueue(MessageQueueEndpoints.SalesOrderQueue);
            inventoryShippedConfiguration.AddQueue(MessageQueueEndpoints.LoggingQueue);

            inventoryShippedConfiguration.InitializeOutboundMessageQueueing();
            messageQueueConfigurations.Add(inventoryShippedConfiguration);

            //
            //	initialize Sending Messages
            //
            IInventoryManagementRepository inventoryManagementDataService = new InventoryManagementRepository();
            IMessageQueueProcessing messageProcessing = new MessageProcessing(inventoryManagementDataService);

            IHostedService sendInventoryManagementMessages = new SendMessages(sendingQueueConnection, messageProcessing,
                messageQueueAppConfig, connectionStrings, messageQueueConfigurations, MessageQueueEndpoints.InventoryQueue);
            //
            //	set up receiving queue
            //
            IMessageQueueConnection receivingConnection = new MessageQueueConnection(messageQueueAppConfig);
            receivingConnection.CreateConnection();

            List<IMessageQueueConfiguration> inboundMessageQueueConfigurations = new List<IMessageQueueConfiguration>();
            IMessageQueueConfiguration inboundConfiguration = new MessageQueueConfiguration(messageQueueAppConfig, receivingConnection);
            inboundMessageQueueConfigurations.Add(inboundConfiguration);

            inboundConfiguration.InitializeInboundMessageQueueing(MessageQueueEndpoints.InventoryQueue);
            inboundConfiguration.InitializeLoggingExchange(MessageQueueExchanges.Logging, MessageQueueEndpoints.LoggingQueue);
            IInventoryManagementRepository inboundInventoryManagementDataService = new InventoryManagementRepository();
            IMessageQueueProcessing inboundMessageProcessing = new MessageProcessing(inboundInventoryManagementDataService);

            IHostedService receiveInventoryManagementMessages = new ReceiveMessages(receivingConnection, inboundMessageProcessing, messageQueueAppConfig, connectionStrings, inboundMessageQueueConfigurations);
            //
            //	Set Up Message Processing
            //
            IInventoryManagementRepository inventoryManagementProcessingDataService = new InventoryManagementRepository();
            IMessageQueueProcessing messageProcessor = new MessageProcessing(inventoryManagementProcessingDataService);
            ProcessMessages processMessages = new ProcessMessages(messageProcessor, messageQueueAppConfig, connectionStrings);

            var builder = new HostBuilder().ConfigureAppConfiguration((hostingContext, config) => { })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<IHostedService>(provider => processMessages);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<IHostedService>(provider => sendInventoryManagementMessages);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<IHostedService>(provider => receiveInventoryManagementMessages);
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    //logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    //logging.AddConsole();
                });

            await builder.RunConsoleAsync();
        }
    }
}