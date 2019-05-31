using System;

namespace MS.Support
{
    public class SupportModel
    {
        public Boolean AccountManagementWebApi { get; set; }

        public Boolean InventoryManagementWebApi { get; set; }
        public Boolean InventoryManagementMessageQueue { get; set; }

        public Boolean SalesOrderManagementWebApi { get; set; }
        public Boolean SalesOrderManagementMessageQueue { get; set; }

        public Boolean PurchaseOrderManagementWebApi { get; set; }
        public Boolean PurchaseOrderManagementMessageQueue { get; set; }

        public Boolean LoggingManagementMessageQueue { get; set; }
    }
}