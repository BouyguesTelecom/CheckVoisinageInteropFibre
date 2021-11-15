using System;

namespace CVI.Service.CVI.Request
{
    /// <summary>
    /// The ExportInterventionsRequest class
    /// </summary>
    public class ExportInterventionsRequest
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}
