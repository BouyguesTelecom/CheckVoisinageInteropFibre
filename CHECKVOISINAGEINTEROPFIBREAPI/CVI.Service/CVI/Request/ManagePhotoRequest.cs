using CVI.Service.CVI.DTO.Photo;

namespace CVI.Service.CVI.Request
{
    /// <summary>
    /// The ManagePhotoRequest class
    /// </summary>
    public class ManagePhotoRequest
    {
        public enums.Action Action { get; set; }

        public PhotoDTO Photo { get; set; }
    }
}
