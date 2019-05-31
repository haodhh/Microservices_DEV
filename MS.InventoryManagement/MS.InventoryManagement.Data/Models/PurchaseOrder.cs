using System;
using System.Collections.Generic;

namespace MS.InventoryManagement.Data.Models
{
    /// <summary>
    /// PurchaseOrder
    /// </summary>
    public class PurchaseOrder
    {
        /// <summary>
        /// 
        /// </summary>
        public int PurchaseOrderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MasterPurchaseOrderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PurchaseOrderNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SupplierName { get; set; }

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
        public int PurchaseOrderStatusId { get; set; }

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
        public PurchaseOrderStatus PurchaseOrderStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    }
}