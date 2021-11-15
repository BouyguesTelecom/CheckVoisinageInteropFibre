using CVI.Domain.ModelBase;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVI.Domain.Model
{
    /// <summary>
    /// The Operator class entity
    /// </summary>
    [Table("REF_OPERATORS")]
    public class Operator: BaseEntity
    {
        /// <summary>
        /// Gets or sets OperatorName
        /// </summary>
        public string OperatorName{ get; set; }

        /// <summary>
        /// Gets or sets Interventions
        /// </summary>
        public IEnumerable<Intervention.Intervention> Interventions { get; set; }
    }
}
