using CVI.Domain.ModelBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVI.Domain.Model.Intervention
{
    /// <summary>
    /// The InterventionType class
    /// </summary>
    [Table("INT_INTERVENTION_TYPES")]
    public class InterventionType: BaseEntity
    {
        /// <summary>
        /// Gets or sets InterventionTypeName
        /// </summary>
        public string InterventionTypeName { get; set; }

        /// <summary>
        /// Gets or sets IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets Duration
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Gets or sets Interventions
        /// </summary>
        public virtual IEnumerable<Intervention> Interventions { get; set; }
    }
}
