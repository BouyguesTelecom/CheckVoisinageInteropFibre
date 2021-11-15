using System;

namespace CVI.Service.CVI.DTO.Intervention
{
    /// <summary>
    /// The InterventionDto class
    /// </summary>
    public class InterventionDTO
    {
        /// <summary>
        /// Gets or sets InterventionId
        /// </summary>
        public int InterventionId{ get; set; }

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

        /// <summary>
        /// Gets or sets HasCodeDecharge
        /// </summary>
        public bool HasCodeDecharge { get; set; }

        #region NAVIGATION PROPS

        /// <summary>
        /// Gets or sets Type
        /// </summary>
        public virtual InterventionTypeDTO Type { get; set; }

        /// <summary>
        /// Gets or sets Operator
        /// </summary>
        public virtual OperatorDTO Operator { get; set; }

        /// <summary>
        /// Gets or sets User
        /// </summary>
        public virtual UserDTO User { get; set; }

        /// <summary>
        /// Gets or sets MutualisationPoint
        /// </summary>
        public virtual MutualisationPointDTO MutualisationPoint { get; set; }

        #endregion
    }
}
