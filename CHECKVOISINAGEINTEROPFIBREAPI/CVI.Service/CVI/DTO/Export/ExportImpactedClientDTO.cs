using System;

namespace CVI.Service.CVI.DTO.Export
{
    /// <summary>
    /// The ExportImpactedClientDTO class
    /// </summary>
    public class ExportImpactedClientDto
    {
        public string InterventionNum { get; set; }

        public string InterventionPm { get; set; }

        public DateTime? PhotoDate { get; set; }

        public string PhotoUser { get; set; }

        public string PhotoDescription { get; set; }

        public int SrvID { get; set; }

        public double SumDurationOfInterruption { get; set; }

        public string AlarmId { get; set; }

        public string AlarmType { get; set; }

        public string AlarmAdress { get; set; }

        public string AlarmOnt { get; set; }

        public DateTime? AlarmStartTime { get; set; }

        public DateTime? AlarmEntTime { get; set; }
    }
}
