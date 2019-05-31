using System;

namespace MS.PurchaseOrderManagement.Data.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public int AccountId { get; set; }
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}