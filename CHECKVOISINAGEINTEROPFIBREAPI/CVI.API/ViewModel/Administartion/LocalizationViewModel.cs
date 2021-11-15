using CVI.Service.CVI.DTO;
using System.Collections.Generic;

namespace CVI.API.ViewModel
{
    /// <summary>
    /// The ManageLocalizationViewModel class
    /// </summary>
    public class LocalizationViewModel
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
        /// Gets or sets SubCauses
        /// </summary>
        public virtual ICollection<SubCauseDTO> SubCauses { get; set; }

    }
}
