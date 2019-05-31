namespace MS.InventoryManagement.Data.Transformations
{
    /// <summary>
    /// ProductInquiryDataTransformation
    /// </summary>
    public class ProductInquiryDataTransformation
    {
        /// <summary>
        /// 
        /// </summary>
        public string ProductNumber { get; set; }

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