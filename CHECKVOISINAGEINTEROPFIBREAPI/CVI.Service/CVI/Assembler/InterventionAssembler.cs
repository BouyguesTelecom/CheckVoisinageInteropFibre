using CVI.Domain.Model.Intervention;
using CVI.Service.CVI.DTO.Intervention;

namespace CVI.Service.CVI.Assembler
{
    /// <summary>
    /// The InterventionAssembler class
    /// </summary>
    public static class InterventionAssembler
    {
        #region TO ENTITY

        /// <summary>
        /// FRom InterventionType To ToInterventionTypeDto
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MutualisationPoint ToMutualisationPoint(this MutualisationPointDTO pmDto)
        {
            return new MutualisationPoint
            {
                ID = pmDto.ID
            };
        }

        /// <summary>
        /// FRom InterventionType To ToInterventionTypeDto
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static InterventionType ToInterventionType(this InterventionTypeDTO type)
        {
            return new InterventionType
            {
                ID = type.ID,
                Duration = new System.TimeSpan(0, type.Duration, 0, 0, 0),
                InterventionTypeName = type.Name,
                IsDeleted = type.IsDeleted,
                CreationDate = type.CreationDate,
            };
        }

        /// <summary>
        /// From Intervention to InterventionDto
        /// </summary>
        /// <param name="intervention"></param>
        /// <returns></returns>
        public static Intervention ToIntervention(this InterventionDTO intervention)
        {
            return new Intervention
            {
                InterventionPto = intervention.InterventionPto,
                BeginDate = intervention.BeginDate,
                CreationDate = intervention.CreationDate,
                InterventionName = intervention.Name,
                UnloadedCode = intervention.UnloadedCode,
                Operator = intervention.Operator?.ToOperator(),
                MutualisationPoint = intervention.MutualisationPoint?.ToMutualisationPoint(),
                InterventionType = intervention.Type?.ToInterventionType(),
                User = intervention.User?.ToUser(),
                ID = intervention.InterventionId
            };
        }

        #endregion

        #region TO DTO

        /// <summary>
        /// FRom InterventionType To ToInterventionTypeDto
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MutualisationPointDTO ToMutualisationPointDto(this MutualisationPoint pm)
        {
            return new MutualisationPointDTO
            {
                ID = pm.ID,
                PMName = pm.PMName
            };
        }

        /// <summary>
        /// FRom InterventionType To ToInterventionTypeDto
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static InterventionTypeDTO ToInterventionTypeDto(this InterventionType type)
        {
            return new InterventionTypeDTO
            {
                ID = type.ID,
                Duration = type.Duration.Hours,
                Name = type.InterventionTypeName,
                IsDeleted = type.IsDeleted,
                CreationDate = type.CreationDate,
            };
        }

        /// <summary>
        /// From Intervention to InterventionDto
        /// </summary>
        /// <param name="intervention"></param>
        /// <returns></returns>
        public static InterventionDTO ToInterventionDto(this Intervention intervention)
        {
            return new InterventionDTO
            {
                InterventionPto = intervention.InterventionPto,
                BeginDate = intervention.BeginDate,
                CreationDate = intervention.CreationDate,
                Name = intervention.InterventionName,
                UnloadedCode = intervention.UnloadedCode,
                Operator = intervention.Operator?.ToOperatorDto(),
                MutualisationPoint = intervention?.MutualisationPoint?.ToMutualisationPointDto(),
                Type = intervention.InterventionType?.ToInterventionTypeDto(),
                User = intervention.User?.ToUserDto(),
                InterventionId = intervention.ID
            };
        }

        #endregion
    }
}
