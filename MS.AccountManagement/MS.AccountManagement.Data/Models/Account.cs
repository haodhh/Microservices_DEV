using System;

namespace MS.AccountManagement.Data.Models
{
    /// <summary>
    /// Account
    /// </summary>
    public class Account
    {
        /// <summary>
        /// 
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DateUpdated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PurchasedApplications { get; set; }
    }
}