using System;

namespace MS.InventoryManagement.Data.Models
{
    /// <summary>
    /// InventoryTransaction
    /// </summary>
    public class InventoryTransaction
    {
        /// <summary>
        /// 
        /// </summary>
        public int InventoryTransactionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double UnitCost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MasterEntityId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime TransactionDate { get; set; }
    }
}