using CVI.Domain.ModelBase;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVI.Domain.Model
{
    /// <summary>
    /// The Alarm entity
    /// </summary>
    [Table("PH_ALARMS")]
    public class Alarm : BaseEntity
    {
        public string NotificationId { get; set; }

        public string AlarmType { get; set; }

        public string Address { get; set; }

        public string ONTNr { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? ClearTime { get; set; }

        #region NAVIGATION PROPS

        [ForeignKey("Photo")]
        public int? PhotoId { get; set; }

        public virtual Photo.Photo Photo { get; set; }

        public virtual ClientResult ClientResult { get; set; }

        #endregion


    }
}
