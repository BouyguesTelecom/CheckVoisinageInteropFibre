using System;

namespace CVI.Service.CVI.DTO.Histo
{
    /// <summary>
    /// The HistoAlarmDTO class
    /// </summary>
    public class HistoAlarmDto
    {
        public string NotificationId { get; set; }

        public string AlarmType { get; set; }

        public string Address { get; set; }

        public string ONTNr { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? ClearTime { get; set; }

        public string Duration { get 
            {
                return ClearTime.HasValue ? string.Format("{0:%d} jours {0:%h} h {0:%m} min {0:%s} s", (StartTime - ClearTime)) : string.Empty ; 
            } 
        }

        public int SrvID { get; set; }
    }
}
