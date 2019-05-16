using System;
using System.Collections;
using System.Collections.Generic;

namespace MS_Common.Models
{
    /// <summary>
    /// ResponseModel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseModel<T>
    {
        /// <summary>
        ///
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool ReturnStatus { get; set; }

        /// <summary>
        ///
        /// </summary>
        public List<String> ReturnMessage { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Hashtable Errors;

        /// <summary>
        ///
        /// </summary>
        public int TotalPages;

        /// <summary>
        ///
        /// </summary>
        public int TotalRows;

        /// <summary>
        ///
        /// </summary>
        public int PageSize;

        /// <summary>
        ///
        /// </summary>
        public Boolean IsAuthenicated;

        /// <summary>
        ///
        /// </summary>
        public T Entity;

        /// <summary>
        /// ResponseModel
        /// </summary>
        public ResponseModel()
        {
            ReturnMessage = new List<String>();
            ReturnStatus = true;
            Errors = new Hashtable();
            TotalPages = 0;
            TotalPages = 0;
            PageSize = 0;
            IsAuthenicated = false;
        }
    }
}