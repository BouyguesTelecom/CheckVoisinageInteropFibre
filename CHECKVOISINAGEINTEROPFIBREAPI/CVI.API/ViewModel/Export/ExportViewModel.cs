using CVI.Service.CVI.DTO.Export;
using System.Collections.Generic;

namespace CVI.API.ViewModel
{
    public class ExportViewModel
    {
        public IEnumerable<ExportInterventionDTO> Interventions { get; set; }

        public IEnumerable<ExportImpactedClientDto> ImpactedClients { get; set; }

        public IEnumerable<ExportKoClientDto> KoClients { get; set; }

    }
}
