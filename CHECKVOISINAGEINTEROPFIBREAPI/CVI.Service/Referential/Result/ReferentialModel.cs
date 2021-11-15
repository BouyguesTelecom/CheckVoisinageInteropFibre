using System;

namespace CVI.Service.Referential.Result
{
    public class ReferentialModel
    {
        public DateTime Date { get; set; }
        public int SrvId { get; set; }
        public string NomNro { get; set; }
        public string PonPath { get; set; }
        public string NomPm { get; set; }
        public string NumONT { get; set; }
        public string ONTPath { get; set; }
        public int IdOSS { get; set; }
        public string LinkStatus { get; set; }
    }
}
