using System;

namespace MS.InventoryManagement.Data.Models
{
    /// <summary>
    /// SalesOrderDetail
    /// </summary>
    public class SalesOrderDetail
    {
        /// <summary>
        /// 
        /// </summary>
        public int SalesOrderDetailId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MasterSalesOrderDetailId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SalesOrderId { get; set; }

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
        public int ShippedQuantity { get; set; }

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
        public SalesOrder SalesOrder { get; set; }
    }
}