namespace CVI.Service.CVI.DTO.Histo
{
    /// <summary>
    /// The HistoClientDTO class
    /// </summary>
    public class HistoClientDTO
    {
        /// <summary>
        /// Gets or sets ServID
        /// </summary>
        public int? SrvID { get; set; }

        /// <summary>
        /// Gets or sets ClientStatus
        /// </summary>
        public string ClientStatus { get; set; }

        /// <summary>
        /// Gets or sets Localization
        /// </summary>
        public string Localization { get; set; }

        /// <summary>
        /// Gets or sets SubCause
        /// </summary>
        public string SubCause { get; set; }

        /// <summary>
        /// Gets or sets Info
        /// </summary>
        public string Info { get; set; }
    }
}
