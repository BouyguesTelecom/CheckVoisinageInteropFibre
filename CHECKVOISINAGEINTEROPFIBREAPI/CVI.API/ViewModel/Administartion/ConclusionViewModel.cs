using System.ComponentModel.DataAnnotations;

namespace CVI.API.ViewModel
{
    /// <summary>
    /// The ManageConclusionViewModel class
    /// </summary>
    public class ConclusionViewModel
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
    }
}
