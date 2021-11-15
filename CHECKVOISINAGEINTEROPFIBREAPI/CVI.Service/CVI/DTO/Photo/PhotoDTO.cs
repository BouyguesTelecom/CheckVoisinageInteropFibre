using System;
using System.Collections.Generic;

namespace CVI.Service.CVI.DTO.Photo
{
    public class PhotoDTO
    { 
        /// <summary>
        /// Gets or sets ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets Date
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets Description
        /// </summary>
        public string Description { get; set; }

        #region Navigation Props

        public Intervention.InterventionDTO Intervention { get; set; }

        /// <summary>
        /// Gets or sets UserLogin
        /// </summary>
        public UserDTO User { get; set; }

        /// <summary>
        /// Gets or sets Alarms
        /// </summary>
        public IEnumerable<AlarmDTO> Alarms { get; set; }

        /// <summary>
        /// Gets or sets PhotoResult
        /// </summary>
        public PhotoResultDTO PhotoResult { get; set; }

        #endregion
    }

}
