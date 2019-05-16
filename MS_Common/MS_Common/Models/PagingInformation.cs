using System;
using System.Collections.Generic;
using System.Text;

namespace MS_Common.Models
{
    /// <summary>
    /// DataGridPagingInformation
    /// </summary>
    public class DataGridPagingInformation
    {
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
        public string SortExpression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SortDirection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalRows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// DataGridPagingInformation
        /// </summary>
        public DataGridPagingInformation()
        {
            CurrentPageNumber = 1;
            PageSize = 10;
            SortDirection = "ASC";
            SortExpression = string.Empty;
            TotalPages = 0;
            TotalRows = 0;
        }
    }
}
