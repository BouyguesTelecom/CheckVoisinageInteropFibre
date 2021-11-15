using System.Collections.Generic;

namespace CVI.Service.CVI.DTO
{
    /// <summary>
    /// The SubCauseDTO class
    /// </summary>
    public class SubCauseDTO
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
        /// Gets or sets Localizations
        /// </summary>
        public virtual ICollection<LocalizationDTO> Localizations { get; set; }

        #endregion
    }
}