using System;

namespace CVI.Service.CVI.DTO.Export
{
    /// <summary>
    /// The ExportInterventionDTO class
    /// </summary>
    public class ExportInterventionDTO
    {
        public string InterventionNum { get; set; }

        public string InterventionPm { get; set; }

        public string InterventionType { get; set; }

        public string InterventionPto { get; set; }

        public DateTime StartDateIntervention { get; set; }

        public DateTime? EndDateIntervention { get; set; }

        public string Clients { get; set; }

        public int RestoredClientsCountOfFirstPhoto { get; set; }

        public int RestoredClientsCountOfOthersPhotos { get; set; }

        public int RestoredClientsCountWithShortCutWith5Alarms { get; set; }

        public int RestoredClientsCountWithShortCutLessThen5Alarms { get; set; }

        public int? RestoredKOClients { get; set; }

        public int NotRestoredCliensCount { get; set; }

        public int TotalCliensCount { get; set; }

        public int DgClientCount { get; set; }

        public string ListDGOFF { get; set; }

        public string FirstCallStatus { get; set; }

        public string ConclusionAndLocalisation { get; set; }
        public string  CreatedBy { get; set; }
    }
}
