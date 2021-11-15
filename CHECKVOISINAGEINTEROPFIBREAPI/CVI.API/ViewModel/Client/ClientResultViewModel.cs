namespace CVI.API.ViewModel
{
    /// <summary>
    /// The ClientResultViewModel class
    /// </summary>
    public class ClientResultViewModel
    {
        /// <summary>
        /// Gets or sets ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets Comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets ProblemOrigin
        /// </summary>
        public string ProblemOrigin { get; set; }

        /// <summary>
        /// Gets or sets IsDown
        /// </summary>
        public bool IsDown { get; set; }

        #region NAVIGATION PROPS

        public virtual int StatusId { get; set; }

        public virtual int LocalizationId { get; set; }

        public virtual int ConclusionId { get; set; }

        public virtual int ClientId { get; set; }

        public virtual int PhotoId { get; set; }

        #endregion
    }
}
