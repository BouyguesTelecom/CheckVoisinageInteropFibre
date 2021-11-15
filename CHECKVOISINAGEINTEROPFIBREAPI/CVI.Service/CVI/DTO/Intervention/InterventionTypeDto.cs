using System;

namespace CVI.Service.CVI.DTO.Intervention
{
    /// <summary>
    /// The InterventionTypeDto class
    /// </summary>
    public class InterventionTypeDTO
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
        /// Gets or sets Duration
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets CreationDate
        /// </summary>
        public DateTime CreationDate { get; set; }
    }
}
