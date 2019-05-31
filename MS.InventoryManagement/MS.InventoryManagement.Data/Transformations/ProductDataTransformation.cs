using System;

namespace MS.InventoryManagement.Data.Transformations
{
    /// <summary>
    /// ProductDataTransformation
    /// </summary>
    public class ProductDataTransformation
    {
        /// <summary>
        /// 
        /// </summary>
        public int AccountId { get; set; }

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
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BinLocation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double UnitPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double AverageCost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int OnHandQuantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int OnOrderQuantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CommittedQuantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DateCreated { get; set; }
    }
}