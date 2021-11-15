using CVI.Domain.Commun;

namespace CVI.API.SendWork
{
    /// <summary>
    /// The ResponseMessage class
    /// </summary>
    public class ResponseMessage
    {
        /// <summary>
        /// Gets or sets ServiceMessage
        /// </summary>
        public ServiceMessage ServiceMessage { get; set; }

        /// <summary>
        /// Gets or sets Object
        /// </summary>
        public object Object { get; set; }


        public ResponseMessage()
        {

        }
        public ResponseMessage(object @object, string message, bool operationSuccess)
        {
            ServiceMessage = new ServiceMessage
            {
                OperationSuccess = operationSuccess,
                ErrorMessage = message
            };
            Object = @object;
        }
    }
}
