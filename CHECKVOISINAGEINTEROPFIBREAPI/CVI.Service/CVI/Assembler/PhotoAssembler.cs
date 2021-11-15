using CVI.Domain.Model.Photo;
using CVI.Service.CVI.DTO.Photo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CVI.Service.CVI.Assembler
{
    /// <summary>
    /// The PhotoAssembler class
    /// </summary>
    public static class PhotoAssembler
    {
        /// <summary>
        /// FROM DTO TO ENTITY
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        public static Photo ToPhoto(this PhotoDTO photo)
        {
            return new Photo
            {
                ID = photo.ID,
                Alarms = photo.Alarms?.Select(a => a?.ToAlarm()).ToList(),
                CreationDate = photo.CreationDate,
                Description = photo.Description,
                Intervention = photo.Intervention?.ToIntervention(),
                PhotoResult = photo.PhotoResult?.ToPhotoResult(),
                User = photo.User?.ToUser()
            };
        }

        /// <summary>
        /// FROM ENTITY TO DTO
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        public static PhotoDTO ToPhotoDto(this Photo photo)
        {
            return new PhotoDTO
            {
                Alarms = photo.Alarms?.Select(a => a?.ToAlarmDTO()).ToList(),
                Intervention = photo.Intervention?.ToInterventionDto(),
                PhotoResult = photo.PhotoResult?.ToPhotoResultDto(),
                CreationDate = photo.CreationDate,
                Description = photo.Description,
                User = photo.User?.ToUserDto(),
                ID = photo.ID
            };
        }
    }
}
