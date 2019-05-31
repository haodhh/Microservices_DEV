using System;
using System.Collections.Generic;
using System.Text;

namespace MS.AccountManagement.Data.Models
{
    /// <summary>
    /// </summaryUser>
    /// 
    /// </summary>
    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int UserTypeId { get; set; }

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
        public DateTime DateLastLogin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Account Account { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UserType UserType { get; set; }
    }
}
