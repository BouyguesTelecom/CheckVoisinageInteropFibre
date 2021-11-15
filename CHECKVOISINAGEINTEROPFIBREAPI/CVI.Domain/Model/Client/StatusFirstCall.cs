using CVI.Domain.ModelBase;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVI.Domain.Model
{
    /// <summary>
    /// The StatusFirstCall class
    /// </summary>
    [Table("CL_CLIENT_STATUS")]
    public class StatusFirstCall: BaseEntity
    {
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