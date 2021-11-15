namespace CVI.API.ViewModel
{
    /// <summary>
    /// The TakePhotoViewModel class
    /// </summary>
    public class UpdateClientResultViewModel
    {
        /// <summary>
        /// Gets or sets ClientResultId
        /// </summary>
        public int ClientResultId { get; set; }

        /// <summary>
        /// Gets or sets SrvID
        /// </summary>
        public int SrvID { get; set; }

        /// <summary>
        /// Gets or sets ONTPath
        /// </summary>
        public string ONTPath { get; set; }

        /// <summary>
        /// Gets or sets StatusFirstCallId
        /// </summary>
        public int? StatusFirstCallId { get; set; } 

        /// <summary>
        /// Gets or sets FirstCallComment
        /// </summary>
        public string FirstCallComment { get; set; }

        /// <summary>
        /// Gets or sets ConclusionId
        /// </summary>
        public int? ConclusionId { get; set; }

        /// <summary>
        /// Gets or sets LocalizationId
        /// </summary>
        public int? LocalizationId { get; set; }

        /// <summary>
        /// Gets or sets SubCauseId
        /// </summary>
        public int? SubCauseId { get; set; }

        /// <summary>
        /// Gets or sets AdditionalInfo
        /// </summary>
        public string AdditionalInfo { get; set; }
    }
}
