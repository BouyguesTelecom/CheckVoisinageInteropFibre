using CVI.Service.AlarmSystem.ItemData;

namespace CVI.Service.AlarmSystem.Request
{
    public class Filter : AlarmItemData
    {
        public string SerialNumber { get; set; }
        public string SLID { get; set; }
    }
}
