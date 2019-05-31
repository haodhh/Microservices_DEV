﻿namespace MS.SalesOrderManagement.Data.Transformations
{
    public class SalesOrderInquiryDataTransformation
    {
        public string CustomerName { get; set; }
        public int CurrentPageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortDirection { get; set; }
        public string SortExpression { get; set; }
    }
}