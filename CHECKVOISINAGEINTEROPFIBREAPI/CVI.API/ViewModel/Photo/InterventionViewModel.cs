using System;

namespace CVI.API.ViewModel
{
    /// <summary>
    /// The InterventionViewModel class
    /// </summary>
    public class InterventionViewModel
    {
        /// <summary>
        /// Gets or sets Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets ArrivalDate
        /// </summary>
        public DateTime? BeginDate { get; set; }

        /// <summary>
        /// Gets or sets CreationDate
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets InterventionPto
        /// </summary>
        public string InterventionPto { get; set; }

        /// <summary>
        /// Gets or sets UnloadedCode
        /// </summary>
        public string UnloadedCode { get; set; }

        #region NAVIGATION PROPS

        public int TypeId { get; set; }

        public int OperatorId { get; set; }

        /// <summary>
        /// Gets or sets PMName
        /// </summary>
        public int PmID { get; set; }

        #endregion
    }
}
