using System;

namespace CVI.Service.CVI.DTO.Histo
{
    /// <summary>
    /// The HistoPhotoDTO class
    /// </summary>
    public class HistoPhotoDTO
    {
        /// <summary>
        /// Gets or sets PhotoID
        /// </summary>
        public int PhotoID { get; set; }

        /// <summary>
        /// Gets or sets User
        /// </summary>
        public UserDTO User { get; set; }

        /// <summary>
        /// Gets or sets CreationDate
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets ImpactedClient
        /// </summary>
        public int ImpactedClient { get; set; }

        /// <summary>
        /// Gets or sets KoClient
        /// </summary>
        public int KoClient { get; set; }
    }
}
