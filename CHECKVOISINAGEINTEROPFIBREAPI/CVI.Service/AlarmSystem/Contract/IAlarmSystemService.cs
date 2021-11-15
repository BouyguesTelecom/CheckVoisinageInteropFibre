using CVI.Domain.Commun;
using CVI.Service.AlarmSystem.ItemData;
using CVI.Service.AlarmSystem.Request;
using CVI.Service.AlarmSystem.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CVI.Service.AlarmSystem.Contract
{
    public interface IAlarmSystemService
    {
        Task<(GetAllAlarmResult, GetAllAlarmsListResult, ServiceMessage)> GetAllAlarms(GetAllAlarmsRequest request);
        Task<(IEnumerable<AlarmItemData>, ServiceMessage)> GetAllFakeAlarms(GetAllFakeAlarmsRequest request);
    }
}
