using System;

namespace MS.PurchaseOrderManagement.Data.Models
{
    public class PurchaseOrderNumberSequence
    {
        public int PurchaseOrderNumberSequenceId { get; set; }
        public int AccountId { get; set; }
        public int PurchaseOrderNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}