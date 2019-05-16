using System;
using System.Collections.Generic;
using System.Text;

namespace MS_Common.Models
{
    /// <summary>
    /// ValidationResult
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// 
        /// </summary>
        public Boolean ValidationStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<String> ValidationMessages { get; set; }

        /// <summary>
        /// ValidationResult
        /// </summary>
        public ValidationResult()
        {
            ValidationStatus = true;
            ValidationMessages = new List<String>();
        }
    }
}
