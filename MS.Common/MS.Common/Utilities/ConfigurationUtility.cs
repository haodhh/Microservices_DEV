using Microsoft.Extensions.Configuration;
using MS.Common.Models;
using System;
using System.IO;

namespace MS.Common.Utilities
{
    /// <summary>
    /// ConfigurationUtility
    /// </summary>
    public static class ConfigurationUtility
    {
        /// <summary>
        /// GetConnectionStrings
        /// </summary>
        /// <returns>ConnectionStrings</returns>
        public static ConnectionStrings GetConnectionStrings()
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            string jsonFile = $"appsettings.{environment}.json";

            if(jsonFile == "appsettings..json")
            {
                jsonFile = "appsettings.json";
            }

            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(jsonFile, optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            ConnectionStrings connectionStrings = new ConnectionStrings();
            configuration.GetSection("ConnectionStrings").Bind(connectionStrings);

            return connectionStrings;
        }
    }
}