using System;
using System.Collections.Generic;

namespace MS.Common.Models.MessageQueuePayloads
{
    /// <summary>
    /// SalesOrderUpdatePayload
    /// </summary>
    public class SalesOrderUpdatePayload
    {
        /// <summary>
        ///
        /// </summary>
        public int SalesOrderId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int SalesOrderNumber { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double OrderTotal { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int SalesOrderStatusId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string SalesOrderStatusDescription { get; set; }

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
        public List<SalesOrderDetailUpdatePayload> SalesOrderDetails { get; set; }
    }
}