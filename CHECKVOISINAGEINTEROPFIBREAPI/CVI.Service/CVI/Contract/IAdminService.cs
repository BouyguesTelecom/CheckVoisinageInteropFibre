using CVI.Domain.Commun;
using CVI.Service.CVI.DTO;
using CVI.Service.CVI.DTO.Intervention;
using CVI.Service.CVI.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CVI.Service.CVI.Contract
{
    public interface IAdminService
    {
        Task<(InterventionTypeDTO, ServiceMessage)> ManageInterventionType(ManageRequest request);

        Task<(IReadOnlyCollection<ConclusionDTO>, ServiceMessage)> GetAllConclusions();

        Task<(ConclusionDTO, ServiceMessage)> ManageConclusion(ManageRequest request);

        Task<(IReadOnlyCollection<StatusFirstCallDTO>, ServiceMessage)> GetAllClientStatus();

        Task<(StatusFirstCallDTO, ServiceMessage)> ManageClientStatus(ManageRequest request);

        Task<(IReadOnlyCollection<LocalizationDTO>, ServiceMessage)> GetAllLocalisations();

        Task<(IReadOnlyCollection<LocalizationDTO>, ServiceMessage)> GetAllLocalisationsBySubCause(GetSubCauseRequest request);

        Task<(LocalizationDTO, ServiceMessage)> GetLocalisation(GetGetLocalizationRequest request);

        Task<(LocalizationDTO, ServiceMessage)> ManageLocalisation(ManageRequest request);

        Task<(IReadOnlyCollection<SubCauseDTO>, ServiceMessage)> GetAllSubCauses();

        Task<(SubCauseDTO, ServiceMessage)> GetSubCause(GetSubCauseRequest request);

        Task<(SubCauseDTO, ServiceMessage)> ManageSubCause(ManageRequest request);
    }
}
