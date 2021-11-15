namespace CVI.Domain.Commun
{
    /// <summary>
    /// The ServiceMessage
    /// </summary>
    public class ServiceMessage
    {
        /// <summary>
        /// Gets or sets a value indicating whether the operation has succeeded.
        /// </summary>
        public bool OperationSuccess { get; set; }

        /// <summary>
        /// Gets or sets ErrorMessage
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
