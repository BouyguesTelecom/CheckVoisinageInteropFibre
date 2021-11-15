using CVI.Service.AlarmSystem.ItemData;
using System.Collections.Generic;

namespace CVI.Service.AlarmSystem.Result
{
    /// <summary>
    /// The GetAllAlarmsResult class
    /// </summary>
    public class GetAllAlarmsListResponse
    {
        public IEnumerable<AlarmItemData> Alarm { get; set; }
    }
}
