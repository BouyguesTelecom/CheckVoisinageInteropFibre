namespace CVI.API.ViewModel
{
    /// <summary>
    /// The ClientStatusViewModel class
    /// </summary>
    public class ClientStatusViewModel
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
