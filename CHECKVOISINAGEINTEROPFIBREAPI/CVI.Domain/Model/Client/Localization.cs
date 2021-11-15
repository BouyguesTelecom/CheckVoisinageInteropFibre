using CVI.Domain.ModelBase;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVI.Domain.Model
{
    /// <summary>
    /// The Localization class
    /// </summary>
    [Table("CL_LOCALISATIONS")]
    public class Localization: BaseEntity
    {
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
        public virtual List<SubCause> SubCauses { get; set; }

        #endregion
    }
}