using CVI.Domain.ModelBase;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVI.Domain.Model.Alarms
{
    [Table("ALARM_NOTIFICATIONS")]
    public class AlarmNotification: BaseEntity
    {
        public string NotificationId { get; set; }
        public string AlarmType { get; set; }
        public string PonPath { get; set; }
        public string Address { get; set; }
        public string ONTNr { get; set; }
        public string StartTime { get; set; }
        public string ClearTime { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime ClearDateTime { get; set; }
    }
}
