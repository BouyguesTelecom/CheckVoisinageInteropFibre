using CVI.Service.CVI.DTO;
using CVI.Service.CVI.DTO.Photo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CVI.Service.CVI.Request
{
    public class ManageClientRequest
    {
        public ClientDTO Client { get; set; }

        public Request.enums.Action Action { get; set; }
    }
}
