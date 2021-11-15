using CVI.Service.CVI.DTO.Histo;
using CVI.Service.CVI.DTO.Photo;
using System.Collections.Generic;

namespace CVI.Service.CVI.DTO.Intervention
{
    /// <summary>
    /// The InterventionHistoricDTO class
    /// </summary>
    public class InterventionHistoricDTO
    {
        /// <summary>
        /// Gets or sets Intervention
        /// </summary>
        public InterventionDTO Intervention { get; set; }

        /// <summary>
        /// Gets or sets GlobalInfo
        /// </summary>
        public InterventionGlobalInfoDTO GlobalInfo { get; set; }

        /// <summary>
        /// Gets or sets KoClients
        /// </summary>
        public IEnumerable<HistoClientDTO> KoClients { get; set; }

        /// <summary>
        /// Gets or sets Photos
        /// </summary>
        public IEnumerable<HistoPhotoDTO> Photos { get; set; }
    }
}
