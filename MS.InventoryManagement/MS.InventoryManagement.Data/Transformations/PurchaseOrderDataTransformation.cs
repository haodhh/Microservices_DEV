using System;
using System.Collections.Generic;

namespace MS.InventoryManagement.Data.Transformations
{
    /// <summary>
    /// PurchaseOrderDataTransformation
    /// </summary>
    public class PurchaseOrderDataTransformation
    {
        /// <summary>
        /// 
        /// </summary>
        public int PurchaseOrderId { get; set; }

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
        public int SupplierId { get; set; }

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
        public string PurchaseOrderStatusDescription { get; set; }

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
        public List<PurchaseOrderDetailDataTransformation> PurchaseOrderDetails { get; set; }
    }
}