namespace MS.Common.Models
{
    /// <summary>
    /// TransactionQueueTypes
    /// </summary>
    public static class TransactionQueueTypes
    {
        /// <summary>
        ///
        /// </summary>
        public static string ProductUpdated = "ProductUpdated";

        /// <summary>
        ///
        /// </summary>
        public static string InventoryReceived = "InventoryReceived";

        /// <summary>
        ///
        /// </summary>
        public static string InventoryShipped = "InventoryShipped";

        /// <summary>
        ///
        /// </summary>
        public static string PurchaseOrderSubmitted = "PurchaseOrderSubmitted";

        /// <summary>
        ///
        /// </summary>
        public static string SalesOrderSubmitted = "SalesOrderSubmitted";

        /// <summary>
        ///
        /// </summary>
        public static string Acknowledgement = "Acknowledgement";

        /// <summary>
        ///
        /// </summary>
        public static string TriggerImmediately = "TriggerImmediately";
    }
}