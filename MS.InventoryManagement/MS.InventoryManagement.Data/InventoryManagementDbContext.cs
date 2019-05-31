using Microsoft.EntityFrameworkCore;
using MS.Common.Models;
using MS.Common.Utilities;
using MS.InventoryManagement.Data.Models;
using System;

namespace MS.InventoryManagement.Data
{
    public class InventoryManagementDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<TransactionQueueInbound> TransactionQueueInbound { get; set; }
        public DbSet<TransactionQueueOutbound> TransactionQueueOutbound { get; set; }
        public DbSet<TransactionQueueInboundHistory> TransactionQueueInboundHistory { get; set; }
        public DbSet<TransactionQueueOutboundHistory> TransactionQueueOutboundHistory { get; set; }
        public DbSet<TransactionQueueSemaphore> TransactionQueueSemaphores { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderStatus> PurchaseOrderStatuses { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderStatus> SalesOrderStatuses { get; set; }
        public DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }

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
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasIndex(u => u.ProductNumber);
            modelBuilder.Entity<TransactionQueueSemaphore>().HasIndex(u => u.SemaphoreKey).IsUnique();

            modelBuilder.Entity<PurchaseOrderStatus>().HasData(
                new { PurchaseOrderStatusId = 1, Description = "Open" },
                new { PurchaseOrderStatusId = 2, Description = "Submitted" },
                new { PurchaseOrderStatusId = 3, Description = "Completed" });

            modelBuilder.Entity<SalesOrderStatus>().HasData(
                new { SalesOrderStatusId = 1, Description = "Open" },
                new { SalesOrderStatusId = 2, Description = "Submitted" },
                new { SalesOrderStatusId = 3, Description = "Completed" });
        }

        public InventoryManagementDbContext(DbContextOptions<InventoryManagementDbContext> options) : base(options)
        {
        }

        public InventoryManagementDbContext()
        {
        }

        /// <summary>
        /// InventoryManagementDbContext
        /// </summary>
        /// <param name="connectionString"></param>
        public InventoryManagementDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}