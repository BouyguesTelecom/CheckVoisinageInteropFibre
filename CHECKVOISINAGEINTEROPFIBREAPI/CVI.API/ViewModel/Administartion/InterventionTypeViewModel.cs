using System;

namespace CVI.API.ViewModel
{
    /// <summary>
    /// The InterventionTypeViewModel class
    /// </summary>
    public class InterventionTypeViewModel
    {
        /// <summary>
        /// Gets or sets ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets InterventionTypeName
        /// </summary>
        public string InterventionTypeName { get; set; }

        /// <summary>
        /// Gets or sets IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets Duration
        /// </summary>
        public int Duration { get; set; }
    }
}
