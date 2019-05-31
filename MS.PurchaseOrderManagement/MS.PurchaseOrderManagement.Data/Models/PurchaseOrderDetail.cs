using System;

namespace MS.PurchaseOrderManagement.Data.Models
{
    public class PurchaseOrderDetail
    {
        public int PurchaseOrderDetailId { get; set; }
        public int PurchaseOrderId { get; set; }
        public int ProductId { get; set; }
        public double UnitPrice { get; set; }
        public int OrderQuantity { get; set; }
        public int ReceiviedQuantity { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public Product Product { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
    }
}