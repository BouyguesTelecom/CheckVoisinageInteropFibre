using CVI.Service.Referential.Result;
using System.Collections.Generic;

namespace CVI.Service.AlarmSystem.Request
{
    public class GetAllFakeAlarmsRequest
    {
        public IEnumerable<ReferentialModel> ReferentialModel { get; set; }
    }
}
