using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CVI.API.SendWork;
using CVI.API.ViewModel;
using CVI.Domain.Commun;
using CVI.Service.CVI.Contract;
using CVI.Service.CVI.DTO;
using CVI.Service.CVI.DTO.Intervention;
using CVI.Service.CVI.Request;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CVI.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        #region PRIVATE PROPS

        /// <summary>
        /// The logger 
        /// </summary>
        private static readonly ILog _logger = LogManager.GetLogger(typeof(AdminController));


        /// <summary>
        /// The _iCVIService
        /// </summary>
        private readonly IAdminService _adminService;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }


        #region TYPE INTERVENTION

        /// <summary>
        /// Create localization
        /// </summary>
        /// <param name="interventionViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateInterventionType([FromBody] InterventionTypeViewModel viewModel)
        {
            string message;
            if (viewModel == null)
            {
                message = $"L'object est invalide.";
                _logger.Debug($"POST /CreateInterventionType - error : {message}");
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            message = CheckCreateInterventionType(viewModel);
            if (message != null)
            {
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            InterventionTypeDTO type = new InterventionTypeDTO
            {
                Duration = viewModel.Duration,
                IsDeleted = viewModel.IsDeleted,
                Name = viewModel.InterventionTypeName,
                CreationDate = DateTime.Now
            };

            var result = await _adminService.ManageInterventionType(new ManageRequest
            {
                Action = Service.CVI.Request.enums.Action.Create,
                Object = type
            });
            return ReturnActionController(ref message, result, "CreateInterventionType");
        }

        /// <summary>
        /// Create localization
        /// </summary>
        /// <param name="interventionViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> UpdateInterventionType([FromBody] InterventionTypeViewModel viewModel)
        {
            string message;
            if (viewModel == null)
            {
                message = $"L'object est invalide.";
                _logger.Debug($"POST /UpdateInterventionType - error : {message}");
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            message = CheckUpdateInterventionType(viewModel);
            if (message != null)
            {
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            InterventionTypeDTO type = new InterventionTypeDTO
            {
                ID = viewModel.ID,
                Duration = viewModel.Duration,
                IsDeleted = viewModel.IsDeleted,
                Name = viewModel.InterventionTypeName,
                CreationDate = DateTime.Now
            };

            var result = await _adminService.ManageInterventionType(new ManageRequest
            {
                Action = Service.CVI.Request.enums.Action.Update,
                Object = type
            });

            return ReturnActionController(ref message, result, "UpdateInterventionType");
        }


        #endregion

        #region CONCLUSION

        /// <summary>
        /// Create conclusion
        /// </summary>
        /// <param name="interventionViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateConclusion([FromBody] ConclusionViewModel viewModel)
        {
            string message;
            if (viewModel == null)
            {
                message = $"L'object est invalide.";
                _logger.Debug($"POST /CreateConclusion - error : {message}");
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            message = CheckCreateConclusion(viewModel);
            if (message != null)
            {
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            ConclusionDTO conclusion = new ConclusionDTO { IsDeleted = viewModel.IsDeleted, Name = viewModel.Name };

            //CREATE Conclusion : 
            var result = await _adminService.ManageConclusion(new Service.CVI.Request.ManageRequest
            {
                Action = Service.CVI.Request.enums.Action.Create,
                Object = conclusion
            });

            return ReturnActionController(ref message, result, "CreateConclusion");

        }

        /// <summary>
        /// Update conclusion
        /// </summary>
        /// <param name="interventionViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> UpdateConclusion([FromBody] ConclusionViewModel viewModel)
        {
            string message;
            if (viewModel == null)
            {
                message = $"L'object est invalide.";
                _logger.Debug($"POST /UpdateConclusion - error : {message}");
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            message = CheckUpdateConclusion(viewModel);
            if (message != null)
            {
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            ConclusionDTO conclusion = new ConclusionDTO
            {
                ID = viewModel.ID,
                IsDeleted = viewModel.IsDeleted,
                Name = viewModel.Name
            };

            //UPDATE CONLUSION : 
            var result = await _adminService.ManageConclusion(new ManageRequest
            {
                Action = Service.CVI.Request.enums.Action.Update,
                Object = conclusion
            });

            return ReturnActionController(ref message, result, "UpdateConclusion");

        }


        /// <summary>
        /// Get all conclusions
        /// </summary>
        /// <param name="interventionViewModel"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetConclusions()
        {
            var result = await _adminService.GetAllConclusions();
            return ReturnActionTypeAllController(result, "GetConclusions");
        }

       

        #endregion

        #region CLIENT STATUS

        /// <summary>
        /// Create client status
        /// </summary>
        /// <param name="interventionViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateClientStatus([FromBody] ClientStatusViewModel viewModel)
        {
            string message;
            if (viewModel == null)
            {
                message = $"L'object est invalide.";
                _logger.Debug($"POST /CreateClientStatus - error : {message}");
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            message = CheckCreateClientStatus(viewModel);
            if (message != null)
            {
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            StatusFirstCallDTO status = new StatusFirstCallDTO
            {
                IsDeleted = viewModel.IsDeleted,
                Name = viewModel.Name,
                IsCommentMandatory = viewModel.IsCommentMandatory
            };

            //CREATE Conclusion : 
            var result = await _adminService.ManageClientStatus(new ManageRequest
            {
                Action = Service.CVI.Request.enums.Action.Create,
                Object = status
            });

            return ReturnActionController(ref message, result, "CreateClientStatus");

        }

        /// <summary>
        /// Update Client status
        /// </summary>
        /// <param name="interventionViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> UpdateClientStatus([FromBody] ClientStatusViewModel viewModel)
        {
            string message;
            if (viewModel == null)
            {
                message = $"L'object est invalide.";
                _logger.Debug($"POST /UpdateClientStatus - error : {message}");
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            message = CheckUpdateClientStatus(viewModel);
            if (message != null)
            {
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            StatusFirstCallDTO status = new StatusFirstCallDTO
            {
                ID = viewModel.ID,
                IsDeleted = viewModel.IsDeleted,
                Name = viewModel.Name,
                IsCommentMandatory = viewModel.IsCommentMandatory,
                IsDefault = viewModel.IsDefault
            };

            //UPDATE STATUS : 
            var result = await _adminService.ManageClientStatus(new ManageRequest
            {
                Action = Service.CVI.Request.enums.Action.Update,
                Object = status
            });

            return ReturnActionController(ref message, result, "UpdateClientStatus");

        }

        /// <summary>
        /// Get all status
        /// </summary>
        /// <param name="interventionViewModel"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetClientStatus()
        {
            var result = await _adminService.GetAllClientStatus();
            return ReturnActionTypeAllController(result, "GetClientStatus");

        }

        #endregion

        #region SUB CAUSE

        /// <summary>
        /// Create sub cause
        /// </summary>
        /// <param name="interventionViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateSubCause([FromBody] SubCauseViewModel viewModel)
        {
            string message;
            if (viewModel == null)
            {
                message = $"L'object est invalide.";
                _logger.Debug($"POST /CreateSubCause - error : {message}");
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            message = CheckCreateSubCause(viewModel);
            if (message != null)
            {
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            SubCauseDTO subCause = new SubCauseDTO { IsDeleted = viewModel.IsDeleted, Name = viewModel.Name };

            //CREATE SUBCAUSE : 
            var result = await _adminService.ManageSubCause(new ManageRequest
            {
                Action = Service.CVI.Request.enums.Action.Create,
                Object = subCause
            });

            return ReturnActionController(ref message, result, "CreateSubCause");

        }

        /// <summary>
        /// Create sub cause
        /// </summary>
        /// <param name="interventionViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> UpdateSubCause([FromBody] SubCauseViewModel viewModel)
        {
            string message;
            if (viewModel == null)
            {
                message = $"L'object est invalide.";
                _logger.Debug($"POST /UpdateSubCause - error : {message}");
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            message = CheckUpdateSubCause(viewModel);
            if (message != null)
            {
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            SubCauseDTO subCause = new SubCauseDTO { IsDeleted = viewModel.IsDeleted, Name = viewModel.Name, ID = viewModel.ID };

            //UPDATE SUBCAUSE : Localisation
            if (subCause.IsDeleted)
            {
                var listLocalisationTOUpdate = await _adminService.GetAllLocalisationsBySubCause(new GetSubCauseRequest
                {
                    SubCauseId = viewModel.ID
                });

                foreach (var localisation in listLocalisationTOUpdate.Item1)
                {
                    LocalizationDTO localization = new LocalizationDTO { ID = localisation.ID, Name = localisation.Name, IsDeleted = localisation.IsDeleted, SubCauses = localisation.SubCauses.Where(s => s.ID != viewModel.ID).ToList() };
                    await _adminService.ManageLocalisation(new ManageRequest
                    {
                        Action = Service.CVI.Request.enums.Action.Update,
                        Object = localization
                    });
                }
            }


            //UPDATE SUBCAUSE : 
            var result = await _adminService.ManageSubCause(new ManageRequest
            {
                Action = Service.CVI.Request.enums.Action.Update,
                Object = subCause
            });

            return ReturnActionController(ref message, result, "UpdateSubCause");

        }

        /// <summary>
        /// Get all sub cause
        /// </summary>
        /// <param name="interventionViewModel"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetSubCauses()
        {
            var result = await _adminService.GetAllSubCauses();
            return ReturnActionTypeAllController(result, "GetSubCauses");

        }

        #endregion

        #region LOCALIZATION

        /// <summary>
        /// Create localization
        /// </summary>
        /// <param name="interventionViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateLocalization([FromBody] LocalizationViewModel viewModel)
        {
            string message;
            if (viewModel == null)
            {
                message = $"L'object est invalide.";
                _logger.Debug($"POST /CreateLocalization - error : {message}");
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            message = CheckCreateLocalization(viewModel);
            if (message != null)
            {
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            IList<SubCauseDTO> subs = viewModel.SubCauses.Select(s => new SubCauseDTO { ID = s.ID }).ToList();

            //CREATE LOCALIZATION : 
            LocalizationDTO localization = new LocalizationDTO
            {
                IsDeleted = viewModel.IsDeleted,
                Name = viewModel.Name,
                SubCauses = subs
            };

            var result = await _adminService.ManageLocalisation(new ManageRequest
            {
                Action = Service.CVI.Request.enums.Action.Create,
                Object = localization
            });

            return ReturnActionController(ref message, result, "CreateLocalization");

        }

        /// <summary>
        /// Change localization
        /// </summary>
        /// <param name="interventionViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> UpdateLocalization([FromBody] LocalizationViewModel viewModel)
        {
            string message;
            if (viewModel == null)
            {
                message = $"L'object est invalide.";
                _logger.Debug($"POST /UpdateLocalization - error : {message}");
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            message = CheckUpdateLocalization(viewModel);
            if (message != null)
            {
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            //INITIALIZE SUB CAUSE : 
            IList<SubCauseDTO> subs = viewModel.SubCauses.ToList();

            //UPDATE LOCALIZATION : 
            LocalizationDTO localization = new LocalizationDTO
            {
                ID = viewModel.ID,
                IsDeleted = viewModel.IsDeleted,
                Name = viewModel.Name,
                SubCauses = subs
            };

            var result = await _adminService.ManageLocalisation(new ManageRequest
            {
                Action = Service.CVI.Request.enums.Action.Update,
                Object = localization
            });

            return ReturnActionController(ref message, result, "UpdateLocalization");

        }

        /// <summary>
        /// Get all localisation
        /// </summary>
        /// <param name="interventionViewModel"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetLocalizations()
        {
            var result = await _adminService.GetAllLocalisations();

            return ReturnActionTypeAllController(result, "GetLocalizations");

        }

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Return type response from action controller
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        private ActionResult ReturnActionTypeAllController<T>((IReadOnlyCollection<T>, ServiceMessage) result, string methodName)
        {
            string message = "";

            if (result.Item2 == null || result.Item1 == null)
            {
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            if (result.Item2 != null && result.Item2?.OperationSuccess == false)
            {
                message = result.Item2.ErrorMessage;
                _logger.Debug($"POST /{methodName} - error : {message}");

                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            return Ok(result.Item1);
        }

        /// <summary>
        /// Return type response from action controller
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="result"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        private ActionResult ReturnActionController<T>(ref string message, (T, ServiceMessage) result, string methodName)
        {
            if (result.Item2 == null || result.Item1 == null)
            {
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            if (result.Item2 != null && result.Item2?.OperationSuccess == false)
            {
                message = result.Item2.ErrorMessage;
                _logger.Debug($"POST /{methodName} - error : {message}");

                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            ResponseMessage response = new ResponseMessage
            {
                ServiceMessage = result.Item2,
                Object = result.Item1
            };

            return Ok(response);
        }

        /// <summary>
        /// Check CreateIntervention Type view model
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private string CheckCreateInterventionType(InterventionTypeViewModel viewModel)
        {
            string message = null;
            if (string.IsNullOrWhiteSpace(viewModel.InterventionTypeName))
            {
                message = $"Le nom est obligatoire.";
                _logger.Debug($"POST / CreateInterventionType - error : {message}");
                return message;
            }
            if (viewModel.Duration < 0)
            {
                message = $"La durée est obligatoire.";
                _logger.Debug($"POST / CreateInterventionType - error : {message}");
                return message;
            }
            return message;
        }

        /// <summary>
        /// Check Update Intervention Type view model
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private string CheckUpdateInterventionType(InterventionTypeViewModel viewModel)
        {
            string message = null;
            if (viewModel.ID <= 0)
            {
                message = $"ID est obligatoire.";
                _logger.Debug($"POST / UpdateInterventionType - error : {message}");
                return message;
            }

            if (string.IsNullOrWhiteSpace(viewModel.InterventionTypeName))
            {
                message = $"Le nom est obligatoire.";
                _logger.Debug($"POST / UpdateInterventionType - error : {message}");
                return message;
            }
            if (viewModel.Duration < 0)
            {
                message = $"La durée est obligatoire.";
                _logger.Debug($"POST / UpdateInterventionType - error : {message}");
                return message;
            }
            return message;
        }


        /// <summary>
        /// check create localisation
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private string CheckCreateLocalization(LocalizationViewModel viewModel)
        {
            string message = null;
            if (string.IsNullOrWhiteSpace(viewModel.Name))
            {
                message = $"Le nom est obligatoire.";
                _logger.Debug($"POST / CreateLocalization - error : {message}");
                return message;
            }
            return message;
        }

        /// <summary>
        /// check create localisation
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private string CheckUpdateLocalization(LocalizationViewModel viewModel)
        {
            string message = null;
            if (viewModel.ID <= 0)
            {
                message = $"Le id est obligatoire.";
                _logger.Debug($"POST / UpdateLocalization - error : {message}");
                return message;
            }

            if (string.IsNullOrWhiteSpace(viewModel.Name))
            {
                message = $"Le nom est obligatoire.";
                _logger.Debug($"POST / UpdateLocalization - error : {message}");
                return message;
            }
            return message;
        }

        /// <summary>
        /// check create conclusion
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private string CheckCreateConclusion(ConclusionViewModel viewModel)
        {
            string message = null;
            if (string.IsNullOrWhiteSpace(viewModel.Name))
            {
                message = $"Le nom est obligatoire.";
                _logger.Debug($"POST / CreateConclusion - error : {message}");
                return message;
            }
            return message;
        }

        /// <summary>
        /// check update conclusion
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private string CheckUpdateConclusion(ConclusionViewModel viewModel)
        {
            string message = null;
            if (string.IsNullOrWhiteSpace(viewModel.Name))
            {
                message = $"Le nom est obligatoire.";
                _logger.Debug($"POST / UpdateConclusion - error : {message}");
                return message;
            }

            if (viewModel.ID <= 0)
            {
                message = $"Le id est obligatoire.";
                _logger.Debug($"POST / UpdateConclusion - error : {message}");
                return message;
            }
            return message;
        }

        /// <summary>
        /// Chack ManageClientStatusViewModel ogject
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private string CheckCreateClientStatus(ClientStatusViewModel viewModel)
        {
            string message = null;
            if (string.IsNullOrWhiteSpace(viewModel.Name))
            {
                message = $"Le nom est obligatoire.";
                _logger.Debug($"POST / CreateClientStatus - error : {message}");
                return message;
            }

            return message;
        }

        /// <summary>
        /// Chack ManageClientStatusViewModel ogject
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private string CheckUpdateClientStatus(ClientStatusViewModel viewModel)
        {
            string message = null;
            if (viewModel.ID <= 0)
            {
                message = $"Le id est obligatoire.";
                _logger.Debug($"POST / UpdateClientStatus - error : {message}");
                return message;
            }

            if (string.IsNullOrWhiteSpace(viewModel.Name))
            {
                message = $"Le nom est obligatoire.";
                _logger.Debug($"POST / UpdateClientStatus - error : {message}");
                return message;
            }

            return message;
        }

        /// <summary>
        /// Check Create SubCause 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private string CheckCreateSubCause(SubCauseViewModel viewModel)
        {
            string message = null;
            if (string.IsNullOrWhiteSpace(viewModel.Name))
            {
                message = $"Le nom est obligatoire.";
                _logger.Debug($"POST / CreateSubCause - error : {message}");
                return message;
            }
            return message;
        }

        /// <summary>
        /// Check Update SubCause
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private string CheckUpdateSubCause(SubCauseViewModel viewModel)
        {
            string message = null;
            if (viewModel.ID <= 0)
            {
                message = $"Le ID est obligatoire.";
                _logger.Debug($"POST / UpdateSubCause - error : {message}");
                return message;
            }

            if (string.IsNullOrWhiteSpace(viewModel.Name))
            {
                message = $"Le nom est obligatoire.";
                _logger.Debug($"POST / CreateSubCause - error : {message}");
                return message;
            }
            return message;
        }

        #endregion
    }
}
