using System;

namespace MS.SalesOrderManagement.Data.Models
{
    public class SalesOrderDetail
    {
        public int SalesOrderDetailId { get; set; }
        public int SalesOrderId { get; set; }
        public int ProductId { get; set; }
        public double UnitPrice { get; set; }
        public int OrderQuantity { get; set; }
        public int ShippedQuantity { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public Product Product { get; set; }
        public SalesOrder SalesOrder { get; set; }
    }
}