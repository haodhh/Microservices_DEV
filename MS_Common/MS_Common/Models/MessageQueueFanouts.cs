using System;
using System.Collections.Generic;
using System.Text;

namespace MS_Common.Models
{
    /// <summary>
    /// MessageQueueFanouts
    /// </summary>
    public static class MessageQueueFanouts
    {
        /// <summary>
        /// 
        /// </summary>
        public static int ProductUpdated = 2;

        /// <summary>
        /// 
        /// </summary>
        public static int PurchaseOrderSubmitted = 1;

        /// <summary>
        /// 
        /// </summary>
        public static int SalesOrderSubmitted = 1;

        /// <summary>
        /// 
        /// </summary>
        public static int InventoryReceived = 2;

        /// <summary>
        /// 
        /// </summary>
        public static int InventoryShipped = 1;
    }
}
