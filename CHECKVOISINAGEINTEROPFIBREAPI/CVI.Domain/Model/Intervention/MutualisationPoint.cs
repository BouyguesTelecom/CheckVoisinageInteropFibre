using CVI.Domain.ModelBase;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVI.Domain.Model.Intervention
{
    /// <summary>
    /// The MutualisationPoint class
    /// </summary>
    [Table("INT_INTERVENTION_PMS")]
    public class MutualisationPoint: BaseEntity
    {
        /// <summary>
        /// Gets or sets PMName
        /// </summary>
        public string PMName { get; set; }
    }
}
