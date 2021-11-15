namespace CVI.Service.AlarmSystem.Request
{
    public class GetAllAlarmsRequest
    {
        public Filter Filter { get; set; }
        public string StartIndex { get; set; }
        public string Size { get; set; }
        public string SortField { get; set; }
        public string Asc { get; set; }
    }
}
