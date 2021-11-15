using CVI.Domain.ModelBase;
using CVI.Service.CVI.DTO.Photo;
using System;

namespace CVI.Service.CVI.DTO
{
    /// <summary>
    /// The Alarm entity
    /// </summary>
    public class AlarmDTO
    {
        public int ID { get; set; }
        public DateTime ConsultationDate { get; set; }

        public string NotificationId { get; set; }

        public string AlarmType { get; set; }

        public string Address { get; set; }

        public string ONTNr { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? ClearTime { get; set; }

        #region NAVIGATION PROPS

        public virtual ClientResultDto ClientResult  { get; set; }

        #endregion


    }
}
