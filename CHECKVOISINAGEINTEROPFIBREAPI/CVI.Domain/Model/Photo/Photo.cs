using CVI.Domain.ModelBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVI.Domain.Model.Photo
{
    [Table("PH_PHOTOS")]
    public class Photo: BaseEntity
    {
        /// <summary>
        /// Gets or sets Description
        /// </summary>
        public string Description { get; set; }

        #region Navigation Props

        [ForeignKey("Intervention")]
        public int? InterventionId { get; set; }

        public virtual Intervention.Intervention Intervention { get; set; }

        /// <summary>
        /// Gets or sets UserLogin
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets Alarms
        /// </summary>
        public virtual IEnumerable<Alarm> Alarms { get; set; }

        /// <summary>
        /// Gets or sets PhotoResult
        /// </summary>
        public virtual PhotoResult PhotoResult { get; set; }

        #endregion
    }

}
