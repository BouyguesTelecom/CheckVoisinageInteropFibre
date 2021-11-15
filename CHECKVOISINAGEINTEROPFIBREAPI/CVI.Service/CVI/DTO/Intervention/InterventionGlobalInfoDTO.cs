namespace CVI.Service.CVI.DTO.Intervention
{
    /// <summary>
    /// The InterventionGlobalInfoDTO class
    /// </summary>
    public class InterventionGlobalInfoDTO
    {
        /// <summary>
        /// Gets or sets InterventionStatus
        /// </summary>
        public bool InterventionStatus { get; set; }

        /// <summary>
        /// Gets or sets IsClientKo
        /// </summary>
        public bool IsClientKo { get; set; }

        /// <summary>
        /// Gets or sets TamingCutAvg
        /// </summary>
        public double TimingCutAvg { get; set; }

        /// <summary>
        /// Gets or sets Superior20CutCount
        /// </summary>
        public int Superior20CutCount { get; set; }

        /// <summary>
        /// Gets or sets Inferior20CutCount
        /// </summary>
        public int Inferior20CutCount { get; set; }

        /// <summary>
        /// Gets or sets ImpactedClientCount
        /// </summary>
        public int ImpactedClientCount { get; set; }

        /// <summary>
        /// Gets or sets PmCount
        /// </summary>
        public int PmCount { get; set; }
    }
}
