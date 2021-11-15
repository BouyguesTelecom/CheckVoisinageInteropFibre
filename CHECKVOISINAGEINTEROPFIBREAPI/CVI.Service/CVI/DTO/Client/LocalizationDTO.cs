using System.Collections.Generic;

namespace CVI.Service.CVI.DTO
{
    /// <summary>
    /// The LocalizationDto class
    /// </summary>
    public class LocalizationDTO
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

        #region NAVIGATION PROPS

        /// <summary>
        /// Gets or sets SubCauses
        /// </summary>
        public virtual ICollection<SubCauseDTO> SubCauses { get; set; }

        #endregion
    }
}