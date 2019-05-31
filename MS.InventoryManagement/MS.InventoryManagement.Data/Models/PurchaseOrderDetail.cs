using System;

namespace MS.InventoryManagement.Data.Models
{
    /// <summary>
    /// PurchaseOrderDetail
    /// </summary>
    public class PurchaseOrderDetail
    {
        /// <summary>
        ///
        /// </summary>
        public int PurchaseOrderDetailId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int MasterPurchaseOrderDetailId { get; set; }

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
        public int ReceivedQuantity { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double OrderTotal { get; set; }

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
        public Product Product { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public PurchaseOrder PurchaseOrder { get; set; }
    }
}