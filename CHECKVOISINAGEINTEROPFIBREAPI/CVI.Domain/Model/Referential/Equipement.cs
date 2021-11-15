using CVI.Domain.ModelBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CVI.Domain.Model.Referential
{
    [Table("NOT_EQUIPEMENTS")]
    public class Equipement: BaseEntity
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
