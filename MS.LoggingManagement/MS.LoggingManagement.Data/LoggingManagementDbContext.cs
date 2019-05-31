using Microsoft.EntityFrameworkCore;
using MS.Common.Models;
using MS.Common.Utilities;
using MS.LoggingManagement.Data.Models;
using System;

namespace MS.LoggingManagement.Data
{
    public class LoggingManagementDbContext : DbContext
    {
        public DbSet<MessagesSent> MessagesSent { get; set; }
        public DbSet<MessagesReceived> MessagesReceived { get; set; }
        public DbSet<AcknowledgementsQueue> AcknowledgementsQueue { get; set; }
        public DbSet<TransactionQueueSemaphore> TransactionQueueSemaphores { get; set; }

        private readonly string _connectionString;

        /// <summary>
        /// On Configuring
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.WriteLine("Connecting to Database = " + _connectionString);
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                ConnectionStrings connectionStrings = ConfigurationUtility.GetConnectionStrings();
                string databaseConnectionString = connectionStrings.DefaultConnection;
                optionsBuilder.UseSqlServer(databaseConnectionString);
            }
            else
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        /// <summary>
        /// Fluent Api
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionQueueSemaphore>().HasIndex(u => u.SemaphoreKey).IsUnique();
        }

        public LoggingManagementDbContext(DbContextOptions<LoggingManagementDbContext> options) : base(options)
        {
        }

        public LoggingManagementDbContext()
        {
        }

        /// <summary>
        /// Inventory Management Database
        /// </summary>
        /// <param name="connectionStrings"></param>
        public LoggingManagementDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}