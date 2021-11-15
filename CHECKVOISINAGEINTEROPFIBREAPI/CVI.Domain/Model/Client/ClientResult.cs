using CVI.Domain.Model.Enums;
using CVI.Domain.ModelBase;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVI.Domain.Model
{
    /// <summary>
    /// The RejectCause class
    /// </summary>
    [Table("CL_CLIENT_RESULTS")]
    public class ClientResult: BaseEntity
    {
        /// <summary>
        /// Gets or sets SrvID
        /// </summary>
        public int SrvID { get; set; }

        /// <summary>
        /// Gets or sets ONTPath
        /// </summary>
        public string ONTPath { get; set; }

        /// <summary>
        /// Gets or sets ProblemOrigin
        /// </summary>
        public string AdditionalInfos { get; set; }

        /// <summary>
        /// Gets or sets Comment
        /// </summary>
        public string FirstCallComent { get; set; }

        /// <summary>
        /// Gets or sets IsDown
        /// </summary>
        public bool IsDown { get; set; }


        #region NAVIGATION PROPS

        [ForeignKey("StatusFirstCall")]
        public int? StatusFirstCallId { get; set; }
        public virtual StatusFirstCall StatusFirstCall { get; set; }

        [ForeignKey("Localization")]
        public int? LocalizationId { get; set; }
        public virtual Localization Localization { get; set; }

        [ForeignKey("Conclusion")]
        public int? ConclusionId { get; set; }
        public virtual Conclusion Conclusion { get; set; }

        //[ForeignKey("Client")]
        //public int? ClientId { get; set; }
        //public virtual Client Client { get; set; }

        [ForeignKey("SubCause")]
        public int? SubCauseId { get; set; }
        public virtual SubCause SubCause { get; set; }

        [ForeignKey("Intervention")]
        public int InterventionId { get; set; }
        public virtual Intervention.Intervention Intervention { get; set; }

        public ClientSteps ClientStep { get; set; }
        public int IdOSS { get; set; }

        #endregion
    }
}
