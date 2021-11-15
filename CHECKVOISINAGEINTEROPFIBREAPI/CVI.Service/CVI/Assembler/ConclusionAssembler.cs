using CVI.Domain.Model;
using CVI.Service.CVI.DTO;
using System;

namespace CVI.Service.CVI.Assembler
{
    public static class ConclusionAssembler
    {
        /// <summary>
        /// From ConclusionDTO to Conclusion
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Conclusion ToConclusion(this ConclusionDTO conclusion)
        {
            return new Conclusion
            {
                CreationDate = DateTime.Now,
                Name = conclusion.Name,
                ID = conclusion.ID,
                IsDeleted = conclusion.IsDeleted
            };
        }

        /// <summary>
        /// From Conclusion to ConclusionDTO
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static ConclusionDTO ToConclusionDto(this Conclusion conclusion)
        {
            return new ConclusionDTO
            {
                ID = conclusion.ID,
                Name = conclusion.Name,
                IsDeleted = conclusion.IsDeleted
            };
        }
    }
}
