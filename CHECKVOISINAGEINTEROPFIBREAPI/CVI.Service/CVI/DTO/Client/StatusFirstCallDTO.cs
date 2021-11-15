namespace CVI.Service.CVI.DTO
{
    /// <summary>
    /// The StatusFirstCallDTO class
    /// </summary>
    public class StatusFirstCallDTO
    {
        /// <summary>
        /// Gets or sets ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets IsDeleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets IsMandatory
        /// </summary>
        public bool IsCommentMandatory { get; set; }

        /// <summary>
        /// Gets or sets IsDefault
        /// </summary>
        public bool IsDefault { get; set; }
        
    }
}