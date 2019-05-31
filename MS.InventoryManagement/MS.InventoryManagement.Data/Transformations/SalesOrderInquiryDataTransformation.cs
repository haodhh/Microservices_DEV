namespace MS.InventoryManagement.Data.Transformations
{
    /// <summary>
    /// SalesOrderInquiryDataTransformation
    /// </summary>
    public class SalesOrderInquiryDataTransformation
    {
        /// <summary>
        /// 
        /// </summary>
        public string CustomerName { get; set; }

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