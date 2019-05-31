using System;

namespace MS.AccountManagement.Data.Models
{
    /// <summary>
    /// Application
    /// </summary>
    public class Application
    {
        /// <summary>
        ///
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ApplicationCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int ApplicationSeed { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime DateUpdated { get; set; }
    }
}