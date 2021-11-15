using CVI.Domain.Model.Photo;
using CVI.Service.CVI.DTO.Photo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CVI.Service.CVI.Assembler
{
    /// <summary>
    /// The PhotoResultAssembler class
    /// </summary>
    public static class PhotoResultAssembler
    {
        /// <summary>
        /// FROM DTO TO ENTITY
        /// </summary>
        /// <param name="photoResult"></param>
        /// <returns></returns>
        public static PhotoResult ToPhotoResult(this PhotoResultDTO photoResult)
        {
            return new PhotoResult
            {
                AverageDownTime = photoResult.AverageDownTime,
                ClientNumber = photoResult.ClientNumber,
                CreationDate = photoResult.CreationDate,
                DownClientNumber = photoResult.DownClientNumber,
                GreaterTo20DownNumber = photoResult.GreaterTo20DownNumber,
                ID = photoResult.ID,
                LowerTo20DownNumber = photoResult.LowerTo20DownNumber,
                NOKClientNumber = photoResult.NOKClientNumber,
                OperationStatus = photoResult.OperationStatus
            };
        }

        /// <summary>
        /// FROM ENTITY TO DTO
        /// </summary>
        /// <param name="photoResult"></param>
        /// <returns></returns>
        public static PhotoResultDTO ToPhotoResultDto(this PhotoResult photoResult)
        {
            return new PhotoResultDTO
            {
                AverageDownTime = photoResult.AverageDownTime,
                ClientNumber = photoResult.ClientNumber,
                CreationDate = photoResult.CreationDate,
                DownClientNumber = photoResult.DownClientNumber,
                GreaterTo20DownNumber = photoResult.GreaterTo20DownNumber,
                ID = photoResult.ID,
                LowerTo20DownNumber = photoResult.LowerTo20DownNumber,
                NOKClientNumber = photoResult.NOKClientNumber,
                OperationStatus = photoResult.OperationStatus
            };
        }
    }
}
