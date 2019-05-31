using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MS.Common.Interfaces;
using MS.Common.MessageQueues;
using MS.Common.Models;
using MS.SalesOrderManagement.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MS.SalesOrderManagement.MessageQueues
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            //MessageQueueAppConfig messageQueueAppConfig = new MessageQueueAppConfig();
            //ConnectionStrings connectionStrings = new ConnectionStrings();

            //string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //string jsonFile = $"appsettings.{environment}.json";

            //var configBuilder = new ConfigurationBuilder()
            //  .SetBasePath(Directory.GetCurrentDirectory())
            //  .AddJsonFile(jsonFile, optional: true, reloadOnChange: true);

            //IConfigurationRoot configuration = configBuilder.Build();

            //configuration.GetSection("MessageQueueAppConfig").Bind(messageQueueAppConfig);
            //configuration.GetSection("ConnectionStrings").Bind(connectionStrings);

            MessageQueueAppConfig messageQueueAppConfig = SaleAppConfig.CreateMessageQueueAppConfig();
            ConnectionStrings connectionStrings = SaleAppConfig.CreateConnectionStrings();

            //
            //	set up sending queue
            //
            IMessageQueueConnection sendingQueueConnection = new MessageQueueConnection(messageQueueAppConfig);
            sendingQueueConnection.CreateConnection();

            List<IMessageQueueConfiguration> messageQueueConfigurations = new List<IMessageQueueConfiguration>();

            IMessageQueueConfiguration salesOrderSubmittedConfiguration = new MessageQueueConfiguration(MessageQueueExchanges.SalesOrderSubmitted, messageQueueAppConfig, sendingQueueConnection);

            salesOrderSubmittedConfiguration.AddQueue(MessageQueueEndpoints.InventoryQueue);
            salesOrderSubmittedConfiguration.AddQueue(MessageQueueEndpoints.LoggingQueue);

            salesOrderSubmittedConfiguration.InitializeOutboundMessageQueueing();
            messageQueueConfigurations.Add(salesOrderSubmittedConfiguration);

            ISalesOrderManagementRepository salesOrderManagementDataService = new SalesOrderManagementRepository();
            IMessageQueueProcessing messageProcessing = new MessageProcessing(salesOrderManagementDataService);

            IHostedService sendSalesOrderManagementMessages =
                new SendMessages(sendingQueueConnection, messageProcessing, messageQueueAppConfig,
                connectionStrings, messageQueueConfigurations, MessageQueueEndpoints.SalesOrderQueue);

            //
            //	set up receiving queue
            //
            IMessageQueueConnection receivingConnection = new MessageQueueConnection(messageQueueAppConfig);
            receivingConnection.CreateConnection();

            List<IMessageQueueConfiguration> inboundMessageQueueConfigurations = new List<IMessageQueueConfiguration>();
            IMessageQueueConfiguration inboundConfiguration = new MessageQueueConfiguration(messageQueueAppConfig, receivingConnection);
            inboundMessageQueueConfigurations.Add(inboundConfiguration);

            inboundConfiguration.InitializeInboundMessageQueueing(MessageQueueEndpoints.SalesOrderQueue);
            inboundConfiguration.InitializeLoggingExchange(MessageQueueExchanges.Logging, MessageQueueEndpoints.LoggingQueue);
            ISalesOrderManagementRepository inboundSalesOrderManagementDataService = new SalesOrderManagementRepository();
            IMessageQueueProcessing inboundMessageProcessing = new MessageProcessing(inboundSalesOrderManagementDataService);

            IHostedService receiveSalesOrderManagementMessages = new ReceiveMessages(receivingConnection, inboundMessageProcessing, messageQueueAppConfig, connectionStrings, inboundMessageQueueConfigurations);

            //
            //	Set Up Message Processing
            //
            ISalesOrderManagementRepository salesOrderManagementProcessingDataService = new SalesOrderManagementRepository();
            IMessageQueueProcessing messageProcessor = new MessageProcessing(salesOrderManagementProcessingDataService);
            ProcessMessages processMessages = new ProcessMessages(messageProcessor, messageQueueAppConfig, connectionStrings);

            var builder = new HostBuilder().ConfigureAppConfiguration((hostingContext, config) =>
            {
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddTransient<IHostedService>(provider => sendSalesOrderManagementMessages);
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddTransient<IHostedService>(provider => processMessages);
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddTransient<IHostedService>(provider => receiveSalesOrderManagementMessages);
            });

            await builder.RunConsoleAsync();
        }
    }
}