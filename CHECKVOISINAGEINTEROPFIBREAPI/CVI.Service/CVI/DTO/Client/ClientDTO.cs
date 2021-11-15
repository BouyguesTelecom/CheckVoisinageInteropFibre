using System.Collections.Generic;

namespace CVI.Service.CVI.DTO
{
    /// <summary>
    /// The ClientDTO class
    /// </summary>
    public class ClientDTO
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

        #region NAVIGATION PROPS

        public virtual IEnumerable<ClientResultDto> ClientResults { get; set; }

        #endregion

    }
}
