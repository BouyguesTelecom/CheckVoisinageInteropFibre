using CVI.Service.Helpers;
using System;

namespace CVI.Service.AlarmSystem.ItemData
{
    /// <summary>
    /// The AlarmItemData class
    /// </summary>
    public class AlarmItemData
    {
        public string NotificationId { get; set; }
        public string AlarmType { get; set; }
        public string Address { get; set; }
        public string PonPath { get; set; }
        public string ONTNr { get; set; }
        public string StartTime { get; set; }
        public string ClearTime { get; set; }

        public DateTime? StartDateTime { get => DateTimeHelper.TimeStampToDateTime(StartTime); }
        public DateTime? ClearDateTime { get => DateTimeHelper.TimeStampToDateTime(ClearTime); }
    }
}
