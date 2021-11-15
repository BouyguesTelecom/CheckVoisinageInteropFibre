using CVI.Domain.ModelBase;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVI.Domain.Model
{
    /// <summary>
    /// The Conclusion class
    /// </summary>
    [Table("CL_CONCLUSIONS")]
    public class Conclusion: BaseEntity
    {
        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets IsDeleted.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}