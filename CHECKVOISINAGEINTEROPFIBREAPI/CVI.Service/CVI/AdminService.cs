using AutoMapper;
using CVI.Domain.Model;
using CVI.Domain.Model.Intervention;
using CVI.Domain.Commun;
using CVI.Service.CVI.Assembler;
using CVI.Service.CVI.Contract;
using CVI.Service.CVI.DTO;
using CVI.Service.CVI.DTO.Intervention;
using CVI.Service.CVI.Request;
using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVI.Domain.Common;

namespace CVI.Service.CVI
{
    /// <summary>
    /// The AdminService class
    /// </summary>
    public class AdminService : IAdminService
    {
        #region PROPS

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog _logger = LogManager.GetLogger(typeof(AdminService));

        /// <summary>
        /// Automapper
        /// </summary>
        private readonly IMapper _mapper;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public AdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion

        /// <summary>
        /// Manage intervention type
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(InterventionTypeDTO, ServiceMessage)> ManageInterventionType(ManageRequest request)
        {
            ServiceMessage serviceMessage;

            if (request is null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Param est pas valide.",
                    OperationSuccess = false
                };

                _logger.Error("The request is null.");
                return (null, serviceMessage);
            }

            if (request.Object == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "L'objet Intervention type est vide.",
                    OperationSuccess = false
                };
                _logger.Error("The intervetionTypeDto object is null.");
                return (null, serviceMessage);
            }

            if (request.Object.GetType() != typeof(InterventionTypeDTO))
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Le type de la demande n'est pas valide.",
                    OperationSuccess = false
                };
                _logger.Error("The intervetionTypeDto object is null.");
                return (null, serviceMessage);
            }

            InterventionTypeDTO interventionTypeDto = request.Object as InterventionTypeDTO;
            InterventionType typeToManage = interventionTypeDto?.ToInterventionType();

            //SEARCH TYPE : 
            if (request.Action == Request.enums.Action.Update)
            {
                var interventionType = (await _unitOfWork.InterventionTypeRepository.Find(t => t.ID == interventionTypeDto.ID)).FirstOrDefault();

                if (interventionType == null)
                {
                    serviceMessage = new ServiceMessage
                    {
                        ErrorMessage = $"Type d'intervention n'est pas trouvé avec ID { interventionTypeDto.ID }.",
                        OperationSuccess = false
                    };
                    _logger.Error($"No intervetionTypeDto object with th ID { interventionTypeDto.ID }.");
                    return (null, serviceMessage);
                }

                typeToManage = interventionType;
            }

            InterventionType type = new InterventionType();
            switch (request.Action)
            {
                case Request.enums.Action.Create:
                    type = await _unitOfWork.InterventionTypeRepository.CreateAsync(typeToManage);
                    break;
                case Request.enums.Action.Update:
                    if (typeToManage != null) 
                    {
                        typeToManage.InterventionTypeName = interventionTypeDto.Name;
                        typeToManage.IsDeleted = interventionTypeDto.IsDeleted;
                        typeToManage.Duration = new System.TimeSpan(0, interventionTypeDto.Duration, 0, 0, 0);
                        type = await _unitOfWork.InterventionTypeRepository.UpdateAsync(typeToManage, typeToManage.ID);
                    }

                    break;
                case Request.enums.Action.Delete:
                    await _unitOfWork.InterventionTypeRepository.DeleteAsync(typeToManage);
                    break;
                default:
                    break;
            }
            await _unitOfWork.SaveAsync();
            serviceMessage = new ServiceMessage { OperationSuccess = true };
            return (type.ToInterventionTypeDto(), serviceMessage);
        }

        /// <summary>
        /// Get all the conclusions
        /// </summary>
        /// <returns></returns>
        public async Task<(IReadOnlyCollection<ConclusionDTO>, ServiceMessage)> GetAllConclusions()
        {
            ServiceMessage serviceMessage;
            IReadOnlyCollection<Conclusion> results = (await _unitOfWork.ConclusionRepository.FindAll()).ToList();

            serviceMessage = new ServiceMessage { OperationSuccess = true };
            return (results?.Select(l => l.ToConclusionDto()).ToList(), serviceMessage);
        }

        /// <summary>
        /// Manage conclusion
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(ConclusionDTO, ServiceMessage)> ManageConclusion(ManageRequest request)
        {
            ServiceMessage serviceMessage;

            if (request is null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Param est pas valide.",
                    OperationSuccess = false
                };

                _logger.Error("The request is null.");
                return (null, serviceMessage);
            }

            if (request.Object == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Conclusion est vide.",
                    OperationSuccess = false
                };
                _logger.Error("The Conclusion object is null.");
                return (null, serviceMessage);
            }

            if (request.Object.GetType() != typeof(ConclusionDTO))
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Type request type n'est pas valide.",
                    OperationSuccess = false
                };
                _logger.Error("The Conclusion object is not valide.");
                return (null, serviceMessage);
            }

            ConclusionDTO conclusionDto = request.Object as ConclusionDTO;
            Conclusion objToManage = conclusionDto?.ToConclusion();

            Conclusion conslusion = new Conclusion();

            switch (request.Action)
            {
                case Request.enums.Action.Create:
                    _unitOfWork.ConclusionRepository.Attach(objToManage);
                    conslusion = await _unitOfWork.ConclusionRepository.CreateAsync(objToManage);
                    break;
                case Request.enums.Action.Update:
                    if (objToManage != null)
                    {
                        conslusion = await _unitOfWork.ConclusionRepository.UpdateAsync(objToManage, objToManage.ID);
                    }
                    break;
                case Request.enums.Action.Delete:
                    await _unitOfWork.ConclusionRepository.DeleteAsync(objToManage);
                    break;
                default:
                    break;
            }
            await _unitOfWork.SaveAsync();
            serviceMessage = new ServiceMessage { OperationSuccess = true };
            return (conslusion?.ToConclusionDto(), serviceMessage);
        }


        /// <summary>
        /// Get all clients states
        /// </summary>
        /// <returns></returns>
        public async Task<(IReadOnlyCollection<StatusFirstCallDTO>, ServiceMessage)> GetAllClientStatus()
        {
            ServiceMessage serviceMessage;
            IReadOnlyCollection<StatusFirstCall> results = (await _unitOfWork.StatusClientRepository.FindAll()).ToList();

            serviceMessage = new ServiceMessage { OperationSuccess = true };
            return (results?.Select(l => l.ToClientStatusDto()).ToList(), serviceMessage);
        }

        /// <summary>
        /// Manage clients states
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(StatusFirstCallDTO, ServiceMessage)> ManageClientStatus(ManageRequest request)
        {
            ServiceMessage serviceMessage;

            if (request is null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Param est pas valide.",
                    OperationSuccess = false
                };

                _logger.Error("The request is null.");
                return (null, serviceMessage);
            }

            if (request.Object == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Statut est vide.",
                    OperationSuccess = false
                };
                _logger.Error("The StatusFirstCallDTO object is null.");
                return (null, serviceMessage);
            }

            if (request.Object.GetType() != typeof(StatusFirstCallDTO))
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Type request type n'est pas valide.",
                    OperationSuccess = false
                };
                _logger.Error("The StatusFirstCallDTO object is not valide.");
                return (null, serviceMessage);
            }

            StatusFirstCallDTO status = request.Object as StatusFirstCallDTO;
            StatusFirstCall objToManage = status?.ToClientStatus();
            StatusFirstCall result = new StatusFirstCall();

            switch (request.Action)
            {
                case Request.enums.Action.Create:
                    _unitOfWork.StatusClientRepository.Attach(objToManage);
                    result = await _unitOfWork.StatusClientRepository.CreateAsync(objToManage);
                    break;
                case Request.enums.Action.Update:
                    if (objToManage != null)
                    {
                        result = await _unitOfWork.StatusClientRepository.UpdateAsync(objToManage, objToManage.ID); 
                    }
                    break;
                case Request.enums.Action.Delete:
                    await _unitOfWork.StatusClientRepository.DeleteAsync(objToManage);
                    break;
                default:
                    break;
            }
            await _unitOfWork.SaveAsync();
            serviceMessage = new ServiceMessage { OperationSuccess = true };
            return (result?.ToClientStatusDto(), serviceMessage);
        }

        /// <summary>
        /// Get all localizations
        /// </summary>
        /// <returns></returns>
        public async Task<(IReadOnlyCollection<LocalizationDTO>, ServiceMessage)> GetAllLocalisations()
        {
            ServiceMessage serviceMessage;
            IReadOnlyCollection<Localization> results = (await _unitOfWork.LocalizationRepository.FindAll(l => l.SubCauses)).ToList();

            serviceMessage = new ServiceMessage { OperationSuccess = true };
            return (results?.Select(l => l.ToLocalizationDto()).ToList(), serviceMessage);
        }

        /// <summary>
        /// Get all localisations by SubCause
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(IReadOnlyCollection<LocalizationDTO>, ServiceMessage)> GetAllLocalisationsBySubCause(GetSubCauseRequest request)
        {
            ServiceMessage serviceMessage;
            IReadOnlyCollection<Localization> results = (await _unitOfWork.LocalizationRepository.Find(l => l.SubCauses.Any(e => e.ID == request.SubCauseId), l => l.SubCauses)).ToList();

            serviceMessage = new ServiceMessage { OperationSuccess = true };
            return (results?.Select(l => l.ToLocalizationDto()).ToList(), serviceMessage);
        }

        /// <summary>
        /// Manage localization
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(LocalizationDTO, ServiceMessage)> GetLocalisation(GetGetLocalizationRequest request)
        {
            ServiceMessage serviceMessage;

            if (request is null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Param est pas valide.",
                    OperationSuccess = false
                };

                _logger.Error("The request is null.");
                return (null, serviceMessage);
            }

            if (request.LocalizationId <= 0)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Localisation est vide.",
                    OperationSuccess = false
                };
                _logger.Error("The ID is null.");
                return (null, serviceMessage);
            }

            Localization result = (await _unitOfWork.LocalizationRepository.Find(l => l.ID == request.LocalizationId, l => l.SubCauses)).FirstOrDefault();
            serviceMessage = new ServiceMessage { OperationSuccess = true };
            return (result?.ToLocalizationDto(), serviceMessage);
        }

        /// <summary>
        /// Manage localization
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(LocalizationDTO, ServiceMessage)> ManageLocalisation(ManageRequest request)
        {
            ServiceMessage serviceMessage;

            if (request is null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Param est pas valide.",
                    OperationSuccess = false
                };

                _logger.Error("The request is null.");
                return (null, serviceMessage);
            }

            if (request.Object == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Localisation est vide.",
                    OperationSuccess = false
                };
                _logger.Error("The LocalizationDTO object is null.");
                return (null, serviceMessage);
            }

            if (request.Object.GetType() != typeof(LocalizationDTO))
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Type request type n'est pas valide.",
                    OperationSuccess = false
                };
                _logger.Error("The LocalizationDTO object is not valide.");
                return (null, serviceMessage);
            }

            LocalizationDTO localization = request.Object as LocalizationDTO;

            //SEARCHING SUB CAUSE : 
            List<SubCause> subCauses = new List<SubCause>();
            if (localization.SubCauses != null && localization.SubCauses.Any())
            {
                foreach (var item in localization.SubCauses)
                {
                    var sub = (await _unitOfWork.SubCauseRepository.Find(s => s.ID == item.ID)).FirstOrDefault();
                    if (sub == null)
                    {
                        serviceMessage = new ServiceMessage
                        {
                            ErrorMessage = $"Sub cause id { item.ID } existe pas.",
                            OperationSuccess = false
                        };
                        _logger.Error("Sub cause not existe.");
                        return (null, serviceMessage);
                    }

                    subCauses.Add(sub);
                }
            }

            Localization objToManage = localization?.ToLocalization();
            Localization result = new Localization();

            switch (request.Action)
            {
                case Request.enums.Action.Create:
                    objToManage.SubCauses.AddRange(subCauses);
                    result = await _unitOfWork.LocalizationRepository.CreateAsync(objToManage);
                    break;
                case Request.enums.Action.Update:
                    var attached = (await _unitOfWork.LocalizationRepository.Find(l => l.ID == objToManage.ID, l => l.SubCauses)).FirstOrDefault();
                    attached.SubCauses.Clear();
                    await _unitOfWork.SaveAsync();

                    attached.SubCauses.AddRange(subCauses);
                    attached.Name = localization.Name;
                    attached.IsDeleted = localization.IsDeleted;
                    result = await _unitOfWork.LocalizationRepository.UpdateAsync(attached, attached.ID);
                    break;
                case Request.enums.Action.Delete:
                    await _unitOfWork.LocalizationRepository.DeleteAsync(objToManage);
                    break;
                default:
                    break;
            }
            await _unitOfWork.SaveAsync();
            serviceMessage = new ServiceMessage { OperationSuccess = true };
            return (result?.ToLocalizationDto(), serviceMessage);
        }

        /// <summary>
        /// Get all sub causes
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(IReadOnlyCollection<SubCauseDTO>, ServiceMessage)> GetAllSubCauses()
        {
            ServiceMessage serviceMessage;

            IReadOnlyCollection<SubCause> results = (await _unitOfWork.SubCauseRepository.FindAll(l => l.Localizations)).ToList();
            serviceMessage = new ServiceMessage { OperationSuccess = true };
            return (results?.Select(l => l?.ToSubCauseLocalizationDto()).ToList(), serviceMessage);
        }

        /// <summary>
        /// Get a sub cause by a id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(SubCauseDTO, ServiceMessage)> GetSubCause(GetSubCauseRequest request)
        {
            ServiceMessage serviceMessage;

            if (request is null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Param est pas valide.",
                    OperationSuccess = false
                };

                _logger.Error("The request is null.");
                return (null, serviceMessage);
            }

            if (request.SubCauseId <= 0)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Sub cause id est vide.",
                    OperationSuccess = false
                };
                _logger.Error("The id is null.");
                return (null, serviceMessage);
            }

            SubCause result = (await _unitOfWork.SubCauseRepository.Find(s => s.ID == request.SubCauseId)).FirstOrDefault();
            serviceMessage = new ServiceMessage { OperationSuccess = true };
            return (result?.ToSubCauseDto(), serviceMessage);
        }

        /// <summary>
        /// Manage sub cause
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(SubCauseDTO, ServiceMessage)> ManageSubCause(ManageRequest request)
        {
            ServiceMessage serviceMessage;

            if (request is null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Param est pas valide.",
                    OperationSuccess = false
                };

                _logger.Error("The request is null.");
                return (null, serviceMessage);
            }

            if (request.Object == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Sub cause est vide.",
                    OperationSuccess = false
                };
                _logger.Error("The SubCauseDTO object is null.");
                return (null, serviceMessage);
            }

            if (request.Object.GetType() != typeof(SubCauseDTO))
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Type request type n'est pas valide.",
                    OperationSuccess = false
                };
                _logger.Error("The SubCauseDTO object is not valide.");
                return (null, serviceMessage);
            }

            SubCauseDTO subCause = request.Object as SubCauseDTO;
            SubCause objToManage = subCause?.ToSubCause();
            SubCause result = new SubCause();

            switch (request.Action)
            {
                case Request.enums.Action.Create:
                    _unitOfWork.SubCauseRepository.Attach(objToManage);
                    result = await _unitOfWork.SubCauseRepository.CreateAsync(objToManage);
                    break;
                case Request.enums.Action.Update:
                    if(objToManage != null)
                    {
                        result = await _unitOfWork.SubCauseRepository.UpdateAsync(objToManage, objToManage.ID);
                    }
                    break;
                case Request.enums.Action.Delete:
                    await _unitOfWork.SubCauseRepository.DeleteAsync(objToManage);
                    break;
                default:
                    break;
            }
            await _unitOfWork.SaveAsync();
            serviceMessage = new ServiceMessage { OperationSuccess = true };
            return (result?.ToSubCauseDto(), serviceMessage);
        }
    }
}
