using System;

namespace MS.InventoryManagement.Data.Transformations
{
    /// <summary>
    /// SalesOrderDetailDataTransformation
    /// </summary>
    public class SalesOrderDetailDataTransformation
    {
        /// <summary>
        /// 
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SalesOrderDetailId { get; set; }

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
        public int ShippedQuantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CurrentShippedQuantity { get; set; }

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
    }
}