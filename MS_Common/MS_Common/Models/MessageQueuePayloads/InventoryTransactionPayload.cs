using System;

namespace MS_Common.Models.MessageQueuePayloads
{
    /// <summary>
    /// InventoryTransactionPayload
    /// </summary>
    public class InventoryTransactionPayload
    {
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