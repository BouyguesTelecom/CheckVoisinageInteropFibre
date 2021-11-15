using CVI.Domain.Model;
using CVI.Service.CVI.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CVI.Service.CVI.Assembler
{
    /// <summary>
    /// The ClientStatusAssemblerclass
    /// </summary>
    public static class ClientStatusAssembler
    {
        /// <summary>
        /// From DTO to Entity
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static StatusFirstCall ToClientStatus(this StatusFirstCallDTO status)
        {
            return new StatusFirstCall
            {
                CreationDate = DateTime.Now,
                ID = status.ID,
                IsDeleted = status.IsDeleted,
                Name = status.Name,
                IsCommentMandatory = status.IsCommentMandatory,
                IsDefault = status.IsDefault
            };
        }


        /// <summary>
        /// From ENTITY to DTO
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static StatusFirstCallDTO ToClientStatusDto(this StatusFirstCall status)
        {
            return new StatusFirstCallDTO
            {
                ID = status.ID,
                IsDeleted = status.IsDeleted,
                Name = status.Name,
                IsCommentMandatory = status.IsCommentMandatory,
                IsDefault = status.IsDefault
            };
        }
    }
}
