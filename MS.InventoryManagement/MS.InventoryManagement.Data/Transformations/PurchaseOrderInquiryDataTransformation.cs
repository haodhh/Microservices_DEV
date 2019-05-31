namespace MS.InventoryManagement.Data.Transformations
{
    /// <summary>
    /// PurchaseOrderInquiryDataTransformation
    /// </summary>
    public class PurchaseOrderInquiryDataTransformation
    {
        /// <summary>
        /// 
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CurrentPageNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SortDirection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SortExpression { get; set; }
    }
}