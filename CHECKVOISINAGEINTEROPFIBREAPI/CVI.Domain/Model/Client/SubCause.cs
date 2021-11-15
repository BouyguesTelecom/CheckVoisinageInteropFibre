using CVI.Domain.ModelBase;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVI.Domain.Model
{
    /// <summary>
    /// The SubCause class
    /// </summary>
    [Table("CL_SUB_CAUSES")]
    public class SubCause: BaseEntity
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
        /// Gets or sets Localizations
        /// </summary>
        public virtual ICollection<Localization> Localizations { get; set; }

        #endregion
    }
}