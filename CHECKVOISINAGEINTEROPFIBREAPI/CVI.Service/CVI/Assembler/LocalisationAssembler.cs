using CVI.Domain.Model;
using CVI.Service.CVI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CVI.Service.CVI.Assembler
{
    public static class LocalisationAssembler
    {
        /// <summary>
        /// From DTO to ENTITY
        /// </summary>
        /// <param name="subCause"></param>
        /// <returns></returns>
        public static SubCause ToSubCause(this SubCauseDTO subCause)
        {
            return new SubCause
            {
                ID = subCause.ID,
                CreationDate = DateTime.Now,
                IsDeleted = subCause.IsDeleted,
                Name = subCause.Name
            };
        }

        /// <summary>
        /// From ENTITY to DTO
        /// </summary>
        /// <param name="subCause"></param>
        /// <returns></returns>
        public static SubCauseDTO ToSubCauseDto(this SubCause subCause)
        {
            return new SubCauseDTO
            {
                ID = subCause.ID,
                IsDeleted = subCause.IsDeleted,
                Name = subCause.Name
            };
        }


        /// <summary>
        /// From ENTITY to DTO
        /// </summary>
        /// <param name="subCause"></param>
        /// <returns></returns>
        public static SubCauseDTO ToSubCauseLocalizationDto(this SubCause subCause)
        {
            return new SubCauseDTO
            {
                ID = subCause.ID,
                IsDeleted = subCause.IsDeleted,
                Name = subCause.Name,
                Localizations = subCause.Localizations?.Select(s => s?.ToLocalizationDto()).ToList()
            };
        }
        /// <summary>
        /// From DTO To ENTITY
        /// </summary>
        /// <param name="localization"></param>
        /// <returns></returns>
        public static Localization ToLocalization(this LocalizationDTO localization)
        {
            return new Localization
            {
                CreationDate = DateTime.Now,
                ID = localization.ID,
                IsDeleted = localization.IsDeleted,
                Name = localization.Name,
                SubCauses = new List<SubCause>()
            };
        }

        /// <summary>
        /// From ENTITY To DTO
        /// </summary>
        /// <param name="localization"></param>
        /// <returns></returns>
        public static LocalizationDTO ToLocalizationDto(this Localization localization)
        {
            return new LocalizationDTO
            {
                ID = localization.ID,
                IsDeleted = localization.IsDeleted,
                Name = localization.Name,
                SubCauses = localization.SubCauses?.Select(s => s?.ToSubCauseDto()).ToList()
            };
        }
    }
}
