using CVI.Domain.ModelBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVI.Domain.Model.Intervention
{
    /// <summary>
    /// The Intervention class
    /// </summary>
    [Table("INT_INTERVENTIONS")]
    public class Intervention: BaseEntity
    {
        public string InterventionName { get; set; }

        /// <summary>
        /// Gets or sets BeginDate
        /// </summary>
        public DateTime? BeginDate { get; set; }

        /// <summary>
        /// Gets or sets InterventionPto
        /// </summary>
        public string InterventionPto { get; set; }

        /// <summary>
        /// Gets or sets UnloadedCode
        /// </summary>
        public string UnloadedCode { get; set; }

        #region NAVIGATION PROPS

        /// <summary>
        /// Gets or sets InterventionType
        /// </summary>
        public virtual InterventionType InterventionType { get; set; }

        [ForeignKey("InterventionType")]
        public int InterventionTypeId { get; set; }

        /// <summary>
        /// Gets or sets Operator
        /// </summary>
        public virtual Operator Operator { get; set; }

        [ForeignKey("Operator")]
        public int OperatorId { get; set; }

        /// <summary>
        /// Gets or sets User
        /// </summary>
        public virtual User User { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets User
        /// </summary>
        public virtual IEnumerable<Photo.Photo> Photos { get; set; }

        /// <summary>
        /// Gets or sets MutualisationPoint
        /// </summary>
        public virtual MutualisationPoint MutualisationPoint { get; set; }

        [ForeignKey("MutualisationPoint")]
        public int MutualisationPointId { get; set; }

        #endregion
    }
}
