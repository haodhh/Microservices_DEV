using Microsoft.EntityFrameworkCore;
using MS.AccountManagement.Data.Models;
using MS.Common.Models;
using MS.Common.Utilities;
using System;

namespace MS.AccountManagement.Data
{
    public class AccountManagementDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

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
            modelBuilder.Entity<User>().HasIndex(u => u.EmailAddress).IsUnique();
        }

        public AccountManagementDbContext(DbContextOptions<AccountManagementDbContext> options) : base(options)
        {
        }

        public AccountManagementDbContext()
        {
        }

        public AccountManagementDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
