using CVI.Service.CVI.DTO.Photo;
using CVI.Service.Referential.Result;
using CVI.Service.AlarmSystem.ItemData;
using System.Collections.Generic;

namespace CVI.Service.CVI.Request
{
    /// <summary>
    /// The ManagePhotoRequest class
    /// </summary>
    public class FillPhotoResultWithAlarmRequest
    {
        public PhotoDTO PhotoDTO { get; set; }
        public IEnumerable<ReferentialModel> RefResult { get; set; }
        public IEnumerable<AlarmItemData> Alarms { get; set; }
    }
}
