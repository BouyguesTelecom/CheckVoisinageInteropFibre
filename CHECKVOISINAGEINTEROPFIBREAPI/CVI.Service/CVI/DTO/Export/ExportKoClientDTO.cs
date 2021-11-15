using System;

namespace CVI.Service.CVI.DTO.Export
{
    /// <summary>
    /// The ExportClientDTO class
    /// </summary>
    public class ExportKoClientDto
    {
        public string InterventionNum { get; set; }

        public string InterventionPm { get; set; }

        public int SrvID { get; set; }

        public DateTime? StartTimeAlarm { get; set; }

        public bool FinalStatus { get; set; }

        public string FirstCallStatus { get; set; }

        public string FirstCallStatusComment { get; set; }

        public string Conclusion { get; set; }

        public string Location { get; set; }

        public string SubCause { get; set; }

        public string AdditionalInfo { get; set; }
    }
}
