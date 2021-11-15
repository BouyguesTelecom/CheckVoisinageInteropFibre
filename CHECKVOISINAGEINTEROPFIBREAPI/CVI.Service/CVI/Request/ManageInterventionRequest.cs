using CVI.Service.CVI.DTO.Intervention;
using CVI.Service.CVI.Request.enums;

namespace CVI.Service.CVI.Request
{
    /// <summary>
    /// The ManageInterventionRequest class
    /// </summary>
    public class ManageInterventionRequest
    {
        /// <summary>
        /// Gets or sets Action
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        /// Gets or sets Intervention
        /// </summary>
        public InterventionDTO Intervention { get; set; }
    }
}
