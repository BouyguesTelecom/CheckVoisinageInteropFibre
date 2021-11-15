using CVI.Domain.Model;
using CVI.Service.CVI.DTO;
using System;

namespace CVI.Service.CVI.Assembler
{
    /// <summary>
    /// The ClientResultAssembler class
    /// </summary>
    public static class ClientResultAssembler
    {
        /// <summary>
        /// FROM DTO to ENTITY
        /// </summary>
        /// <param name="clientResult"></param>
        /// <returns></returns>
        public static ClientResult ToClientResult(this ClientResultDto clientResult)
        {
            return new ClientResult
            {
                IsDown = clientResult.IsDown,
                AdditionalInfos = clientResult.ProblemOrigin,
                FirstCallComent = clientResult.FirstCallComent,
                CreationDate = DateTime.Now,
                ID = clientResult.ID,
                ConclusionId = clientResult.ConclusionId,
                LocalizationId = clientResult.LocalizationId,
                StatusFirstCallId = clientResult.StatusFirstCallId,
                InterventionId = clientResult.InterventionId,
                SrvID = clientResult.SrvID,
                ONTPath = clientResult.ONTPath,
                IdOSS = clientResult.IdOSS,
                //ClientId = clientResult.ClientId,
                ClientStep = clientResult.ClientStep
            };
        }

        /// <summary>
        /// FROM ENTITY to DTO
        /// </summary>
        /// <param name="clientResult"></param>
        /// <returns></returns>
        public static ClientResultDto ToClientResultDto(this ClientResult clientResult)
        {
            return new ClientResultDto
            {
                IsDown = clientResult.IsDown,
                ProblemOrigin = clientResult.AdditionalInfos,
                FirstCallComent = clientResult.FirstCallComent,
                CreationDate = clientResult.CreationDate,
                ID = clientResult.ID,
                ConclusionId = clientResult.ConclusionId,
                LocalizationId = clientResult.LocalizationId,
                StatusFirstCallId = clientResult.StatusFirstCallId,
                StatusFirstCall = clientResult.StatusFirstCall?.ToClientStatusDto(),
                SubCauseId = clientResult.SubCauseId,
                InterventionId = clientResult.InterventionId,
                //ClientId = clientResult.ClientId,
                SrvID = clientResult.SrvID,
                ONTPath = clientResult.ONTPath,
                IdOSS = clientResult.IdOSS,
                ClientStep = clientResult.ClientStep
                //Client = clientResult.Client?.ToClientDto()
            };
        }
    }
}
