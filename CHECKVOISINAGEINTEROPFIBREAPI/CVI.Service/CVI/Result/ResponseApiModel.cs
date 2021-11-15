using CVI.Domain.Commun;


namespace BDL.Service.BDL.Result
{
    public class ResponseApiModel
    {
        public object Result { get; set; }
        public ServiceMessage ServiceMessage { get; set; }

        public ResponseApiModel(object result, ServiceMessage serviceMessage)
        {
            Result = result;
            ServiceMessage = serviceMessage;
        }
    }
}
