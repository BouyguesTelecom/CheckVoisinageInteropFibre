using CVI.Domain.Commun;
using CVI.Service.CVI.DTO;
using CVI.Service.CVI.DTO.Intervention;
using CVI.Service.CVI.DTO.Photo;
using CVI.Service.CVI.Request;
using System;
using CVI.Service.Referential.Result;
using System.Collections.Generic;
using System.Threading.Tasks;
using CVI.Service.AlarmSystem.ItemData;
using CVI.Service.CVI.DTO.Histo;
using CVI.Service.CVI.DTO.Export;

namespace CVI.Service.CVI.Contract
{
    public interface ICviService
    {
        Task<(IEnumerable<InterventionDTO>, ServiceMessage)> GetAllInterventions();

        Task<(InterventionDTO, ServiceMessage)> GetInterventionByID(int interventionId);

        Task<(InterventionDTO, ServiceMessage)> GetInterventionByName(GetInterventionByNameRequest request);

        Task<(InterventionDTO, ServiceMessage)> ManageIntervention(ManageInterventionRequest request);

        Task<(IEnumerable<InterventionTypeDTO>, ServiceMessage)> GetAllInterventionTypes();

        Task<(InterventionTypeDTO, ServiceMessage)> GetInterventionTypeByName(GetInterventionTypeByNameRequest request);

        Task<(InterventionTypeDTO, ServiceMessage)> GetInterventionType(GetInterventionTypeRequest request);

        Task<(IEnumerable<OperatorDTO>, ServiceMessage)> GetAllOperators();

        Task<(OperatorDTO, ServiceMessage)> GetOperator(GetOperatorRequest request);

        Task<(OperatorDTO, ServiceMessage)> GetOperatorByName(GetOperatorByNameRequest request);
        Task<(InterventionDTO, ServiceMessage)> GetLastInterventionByPmName(GetLastInterventionByPmNameRequest request);

        Task<(UserDTO, ServiceMessage)> GetUser(GetUserRequest request);

        Task<(UserDTO, ServiceMessage)> ManageUser(ManageRequest request);

        Task<(PhotoDTO, ServiceMessage)> ManagePhoto(ManagePhotoRequest request);

        Task<(ClientResultDto, ServiceMessage)> ManageClientResult(ManageRequest request);

        Task<(UserDTO, ServiceMessage)> GetLoggedUser(GetCVILoggedUserRequest request);

        Task<(IEnumerable<PhotoDTO>, ServiceMessage)> GetPhotosByIntervention(GetPhotosByInterventionRequest request);       
        
        string GenerateCodeDecharge(DateTime? dateTime);

        Task<(PhotoDTO, ServiceMessage)> SavePhotoResultAndAlarms(FillPhotoResultWithAlarmRequest request);

        Task<(MutualisationPointDTO, ServiceMessage)> ManageMutualisationPoint(ManageRequest request);

        Task<(IEnumerable<MutualisationPointDTO>, ServiceMessage)> GetAllMutualisationPoints();
        Task<(MutualisationPointDTO, ServiceMessage)> GetMutualisationPointbyName(string name);

        Task<(MutualisationPointDTO, ServiceMessage)> GetMutualisationPoint(GetMutualisationPointRequest request);

        Task<(IEnumerable<InterventionDTO>, ServiceMessage)> GetInterventionByPm(GetInterventionByPmRequest request);

        Task<(IEnumerable<ClientResultDto>, ServiceMessage)> GetClientsKoByIntervention(int interventionId);

        Task<(InterventionHistoricDTO, ServiceMessage)> GetInterventionHistoric(GetInterventionHistoricRequest request);

        Task<(IEnumerable<HistoAlarmDto>, ServiceMessage)> GetHistoricAlarms(GetHistoricAlarmRequest request);

        Task<(IEnumerable<ExportInterventionDTO>, IEnumerable<ExportImpactedClientDto>, List<ExportKoClientDto>, ServiceMessage)> ExportInterventions(ExportInterventionsRequest request);

    }
}
