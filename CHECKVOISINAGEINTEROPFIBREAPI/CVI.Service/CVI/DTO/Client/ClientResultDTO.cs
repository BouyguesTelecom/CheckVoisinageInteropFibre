using CVI.Domain.Model.Enums;
using System;

namespace CVI.Service.CVI.DTO
{
    /// <summary>
    /// The ClientResultDTO class
    /// </summary>
    public class ClientResultDto
    {
        /// <summary>
        /// Gets or sets ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets SrvID
        /// </summary>
        public int SrvID { get; set; }

        /// <summary>
        /// Gets or sets ONTPath
        /// </summary>
        public string ONTPath { get; set; }
        /// <summary>
        /// Gets or sets Comment
        /// </summary>
        public string FirstCallComent { get; set; }

        /// <summary>
        /// Gets or sets ProblemOrigin
        /// </summary>
        public string ProblemOrigin { get; set; }

        /// <summary>
        /// Gets or sets CreationDate
        /// </summary>
        public DateTime CreationDate { get; set; }

        public ClientSteps ClientStep { get; set; }

        /// <summary>
        /// Gets or sets IsDown
        /// </summary>
        public bool IsDown { get; set; }

        /// <summary>
        /// Gets or sets LinkStatus
        /// </summary>
        public string LinkStatus { get; set; }
        public int IdOSS { get; set; }

        #region NAVIGATION PROPS

        public int? SubCauseId { get; set; }
        public int? StatusFirstCallId { get; set; }
        public StatusFirstCallDTO StatusFirstCall { get; set; }

        public int? LocalizationId { get; set; }

        public int? ConclusionId { get; set; }

        public int InterventionId { get; set; }

        #endregion
    }
}