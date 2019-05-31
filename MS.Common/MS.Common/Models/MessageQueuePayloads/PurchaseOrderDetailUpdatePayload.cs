using System;

namespace MS.Common.Models.MessageQueuePayloads
{
    /// <summary>
    /// PurchaseOrderDetailUpdatePayload
    /// </summary>
    public class PurchaseOrderDetailUpdatePayload
    {
        /// <summary>
        ///
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int PurchaseOrderDetailId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int PurchaseOrderId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int ProductMasterId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ProductNumber { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ProductDescription { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double UnitPrice { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int OrderQuantity { get; set; }

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