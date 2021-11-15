using System;
using System.ComponentModel.DataAnnotations;

namespace CVI.Domain.ModelBase
{
    /// <summary>
    /// The BaseEntity class
    /// </summary>
    public abstract class BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets CreationDate
        /// </summary>
        public DateTime CreationDate { get; set; }

    }
}
