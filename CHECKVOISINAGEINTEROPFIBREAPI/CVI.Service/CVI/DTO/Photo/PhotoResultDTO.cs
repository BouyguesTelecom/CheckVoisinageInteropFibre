using CVI.Domain.ModelBase;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVI.Service.CVI.DTO.Photo
{
    /// <summary>
    /// The PhotoResult entity class
    /// </summary>
    public class PhotoResultDTO: BaseEntity
    {
        public int ClientNumber { get; set; }

        public int DownClientNumber { get; set; }

        public int NOKClientNumber { get; set; }

        public long AverageDownTime { get; set; }

        public int LowerTo20DownNumber{ get; set; }

        public int GreaterTo20DownNumber{ get; set; }

        public string OperationStatus { get; set; }
    }
}
