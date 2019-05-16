namespace MS_Common.Models.MessageQueuePayloads
{
    /// <summary>
    /// ProductUpdatePayload
    /// </summary>
    public class ProductUpdatePayload
    {
        /// <summary>
        /// 
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProductNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BinLocation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double UnitPrice { get; set; }
    }
}