using System;

namespace MS.SalesOrderManagement.Data.Models
{
    public class Product
    {
        public int AccountId { get; set; }
        public int ProductId { get; set; }
        public int ProductMasterId { get; set; }
        public string ProductNumber { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public int OnHandQuantity { get; set; }
        public int CommittedQuantity { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}