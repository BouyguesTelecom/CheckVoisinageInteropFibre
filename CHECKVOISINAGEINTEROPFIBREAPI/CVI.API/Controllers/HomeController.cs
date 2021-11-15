using CVI.API.SendWork;
using CVI.API.ViewModel;
using CVI.Service.CVI.Contract;
using CVI.Service.CVI.DTO;
using CVI.Service.CVI.DTO.Intervention;
using CVI.Service.CVI.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CVI.Service.CVI.DTO.Photo;
using CVI.Service.Referential.Contract;
using CVI.Service.AlarmSystem.Contract;
using CVI.Service.AlarmSystem.Request;
using CVI.Service.AlarmSystem.Result;
using System.Linq;
using log4net;
using CVI.Domain.Commun;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using CVI.Service.CVI.DTO.Export;

namespace CVI.API.Controllers
{
    /// <summary>
    /// The HomeController class
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        #region PRIVATE PROPS

        /// <summary>
        /// The logger 
        /// </summary>
        private static readonly ILog _logger = LogManager.GetLogger(typeof(HomeController));

        /// <summary>
        /// The _iCVIService
        /// </summary>
        private readonly ICviService _iCVIService;

        /// <summary>
        /// _referentialService
        /// </summary>
        private readonly IReferentialService _referentialService;

        /// <summary>
        /// _alarmService
        /// </summary>
        private readonly IAlarmSystemService _alarmService;


        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public HomeController(ICviService iCVIService, IReferentialService referentialService, IAlarmSystemService alarmSystem)
        {
            _referentialService = referentialService;
            _iCVIService = iCVIService;
            _alarmService = alarmSystem;
        }

        /// <summary>
        /// Gets a collection of intervention type.
        /// </summary>
        /// <response code="200">Returns list of Ticket</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<InterventionDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<InterventionDTO>>> GetAllInterventions()
        {
            var result = await _iCVIService.GetAllInterventions();
            string message;

            if (result.Item2 == null || result.Item1 == null)
            {
                message = $"Les types sont introuvables.";
                _logger.Error($"POST /GetInterventions - error : {message}");

                return NotFound(new ErrorMessage { Code = "NotFound", Message = message });
            }

            if (result.Item2?.OperationSuccess == false)
            {
                message = result.Item2.ErrorMessage;
                _logger.Error($"POST /GetInterventions - error : {message}");

                return NotFound(new ErrorMessage { Code = "NotFound", Message = message });
            }
            return Ok(result.Item1);
        }


        /// <summary>
        /// Create Intervention
        /// </summary>
        /// <param name="interventionViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ResponseMessage>> CreateIntervention([FromBody] InterventionViewModel interventionViewModel)
        {
            ResponseMessage response;
            string message;

            if (interventionViewModel == null)
            {
                message = $"L'object est invalide.";
                _logger.Error($"POST /CreateIntervention - error : {message}");

                response = new ResponseMessage
                {
                    Object = null,
                    ServiceMessage = new ServiceMessage
                    {
                        OperationSuccess = false,
                        ErrorMessage = message
                    }
                };
                return Ok(response);
            }

            var interventionResult = await _iCVIService.GetInterventionByName(new GetInterventionByNameRequest { Name = interventionViewModel.Name });

            if (interventionResult.Item2.OperationSuccess)
            {
                response = new ResponseMessage
                {
                    Object = null,
                    ServiceMessage = new ServiceMessage
                    {
                        OperationSuccess = false,
                        ErrorMessage = "Le numéro d'intervention existe déjà"
                    }
                };
                return Ok(response);
            }

            message = CheckCreateInterventionRequired(interventionViewModel);
            if (message != null)
            {
                response = new ResponseMessage
                {
                    Object = null,
                    ServiceMessage = new ServiceMessage
                    {
                        OperationSuccess = false,
                        ErrorMessage = message
                    }
                };
                return Ok(response);
            }

            //FIND TYPE : 
            var typeResult = await _iCVIService.GetInterventionType(new GetInterventionTypeRequest
            {
                TypeId = interventionViewModel.TypeId
            });

            if (typeResult.Item2 == null && typeResult.Item1 == null)
            {
                message = $"Type de intervention est introuvable.";
                _logger.Error($"POST /CreateIntervention - error : {message}");

                response = new ResponseMessage
                {
                    Object = null,
                    ServiceMessage = new ServiceMessage
                    {
                        OperationSuccess = false,
                        ErrorMessage = message
                    }
                };
                return Ok(response);
            }

            if (typeResult.Item2 != null && typeResult.Item2?.OperationSuccess == false)
            {
                message = typeResult.Item2.ErrorMessage;
                _logger.Error($"POST /CreateIntervention - error : {message}");

                response = new ResponseMessage
                {
                    Object = null,
                    ServiceMessage = new ServiceMessage
                    {
                        OperationSuccess = false,
                        ErrorMessage = message
                    }
                };
                return Ok(response);
            }

            //FIND OPERATOR : 
            var operatorResult = await _iCVIService.GetOperator(new GetOperatorRequest
            {
                OperatorId = interventionViewModel.OperatorId
            });

            if (operatorResult.Item2 == null && operatorResult.Item1 == null)
            {
                message = $"Operateur est introuvable.";
                _logger.Error($"POST /CreateIntervention - error : {message}");

                response = new ResponseMessage
                {
                    Object = null,
                    ServiceMessage = new ServiceMessage
                    {
                        OperationSuccess = false,
                        ErrorMessage = message
                    }
                };
                return Ok(response);
            }

            if (operatorResult.Item2 != null && operatorResult.Item2?.OperationSuccess == false)
            {
                message = operatorResult.Item2.ErrorMessage;
                _logger.Error($"POST /CreateIntervention - error : {message}");

                response = new ResponseMessage
                {
                    Object = null,
                    ServiceMessage = new ServiceMessage
                    {
                        OperationSuccess = false,
                        ErrorMessage = message
                    }
                };
                return Ok(response);
            }

            //FIND CVI USER : 
            var userResult = await UserTreatments("CreateIntervention");

            if (userResult != null && !userResult.ServiceMessage.OperationSuccess)
            {
                return userResult;
            }

            //FIND PM : 
            var pmResult = await _iCVIService.GetMutualisationPoint(new GetMutualisationPointRequest
            {
                PmId = interventionViewModel.PmID
            });

            if (pmResult.Item2 == null || pmResult.Item1 == null)
            {
                message = $"PM est invalide.";
                _logger.Error($"POST /CreateIntervention - error : {message}");

                response = new ResponseMessage
                {
                    Object = null,
                    ServiceMessage = new ServiceMessage
                    {
                        OperationSuccess = false,
                        ErrorMessage = message
                    }
                };
                return Ok(response);
            }

            if (pmResult.Item2 != null && !pmResult.Item2.OperationSuccess)
            {
                message = userResult?.ServiceMessage.ErrorMessage;
                _logger.Error($"POST /CreateIntervention - error : {message}");

                response = new ResponseMessage
                {
                    Object = null,
                    ServiceMessage = new ServiceMessage
                    {
                        OperationSuccess = false,
                        ErrorMessage = message
                    }
                };
                return Ok(response);
            }

            //CREATE INTERVENTION : 
            var result = await _iCVIService.ManageIntervention(new ManageInterventionRequest
            {
                Action = Service.CVI.Request.enums.Action.Create,

                Intervention = new InterventionDTO
                {
                    InterventionPto = interventionViewModel.InterventionPto,
                    BeginDate = interventionViewModel.BeginDate.HasValue ? interventionViewModel.BeginDate.Value.ToLocalTime() : interventionViewModel.BeginDate,
                    CreationDate = DateTime.Now,
                    Name = interventionViewModel.Name,
                    UnloadedCode = interventionViewModel.UnloadedCode,
                    MutualisationPoint = pmResult.Item1,
                    Operator = operatorResult.Item1,
                    Type = typeResult.Item1,
                    User = userResult?.Object as UserDTO
                }
            });

            if (result.Item2 == null && result.Item1 == null)
            {
                message = $"Impossible d'jouter intervention.";
                _logger.Error($"POST /CreateIntervention - error : {message}");

                response = new ResponseMessage
                {
                    Object = null,
                    ServiceMessage = new ServiceMessage
                    {
                        OperationSuccess = false,
                        ErrorMessage = message
                    }
                };
                return Ok(response);
            }

            if (result.Item2 != null && result.Item2?.OperationSuccess == false)
            {
                message = result.Item2.ErrorMessage;
                _logger.Error($"POST /CreateIntervention - error : {message}");

                response = new ResponseMessage
                {
                    Object = null,
                    ServiceMessage = new ServiceMessage
                    {
                        OperationSuccess = false,
                        ErrorMessage = message
                    }
                };
                return Ok(response);
            }

            response = new ResponseMessage
            {
                Object = result.Item1,
                ServiceMessage = new ServiceMessage
                {
                    OperationSuccess = true
                }
            };

            return Ok(response);
        }

        /// <summary>
        /// Gets a collection of intervention type.
        /// </summary>
        /// <response code="200">Returns list of Ticket</response>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ResponseMessage>> GetInterventionTypes()
        {
            var result = await _iCVIService.GetAllInterventionTypes();
            return ReturnActionController(result, "GetMutualisationPointByName");
        }


        /// <summary>
        /// Gets a collection of intervention type.
        /// </summary>
        /// <response code="200">Returns list of Ticket</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MutualisationPointDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ResponseMessage>> GetMutualisationPoints()
        {
            var result = await _iCVIService.GetAllMutualisationPoints();

            return ReturnActionController(result, "GetMutualisationPoints");

        }

        [HttpGet]
        [ProducesResponseType(typeof(MutualisationPointDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        [Route("{name}")]
        public async Task<ActionResult<ResponseMessage>> GetMutualisationPointByName(string name)
        {
            var result = await _iCVIService.GetMutualisationPointbyName(name);

            return ReturnActionController(result, "GetMutualisationPointByName");
        }
        [HttpGet]
        [ProducesResponseType(typeof(InterventionTypeDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        [Route("{name}")]
        public async Task<ActionResult<ResponseMessage>> GetInterventionTypeByName(string name)
        {
            var result = await _iCVIService.GetInterventionTypeByName(new GetInterventionTypeByNameRequest
            {
                TypeName = name
            });
            return ReturnActionController(result, "GetInterventionTypeByName");
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(OperatorDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        [Route("{name}")]
        public async Task<ActionResult<ResponseMessage>> GetOperatorByName(string name)
        {
            var result = await _iCVIService.GetOperatorByName(new GetOperatorByNameRequest
            {
                Name = name
            });
            return ReturnActionController(result, "GetOperatorByName");

        }

        /// <summary>
        /// Gets a collection of operator.
        /// </summary>
        /// <response code="200">Returns list of Ticket</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OperatorDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ResponseMessage>> GetOperators()
        {
            var result = await _iCVIService.GetAllOperators();
            return ReturnActionController(result, "GetOperators");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<InterventionDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        [Route("{name}")]
        public async Task<ActionResult<ResponseMessage>> GetLastInterventionByPmName(string name)
        {
            var result = await _iCVIService.GetLastInterventionByPmName(new GetLastInterventionByPmNameRequest
            {
                Name = name
            });
            return ReturnActionController(result, "GetLastInterventionByPmName");

        }

        /// <summary>
        /// Create and return photo
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ResponseMessage>> TakePhoto(TakePhotoViewModel viewModel)
        {
            try
            {
                ResponseMessage response;
                string message;

                if (viewModel == null)
                {
                    message = $"L'object est invalide.";
                    _logger.Error($"POST /TakePhoto - error : {message}");

                    response = new ResponseMessage
                    {
                        Object = null,
                        ServiceMessage = new ServiceMessage
                        {
                            OperationSuccess = false,
                            ErrorMessage = message
                        }
                    };
                    return Ok(response);
                }

                message = CheckTakePhotoRequired(viewModel);
                if (message != null)
                {
                    response = new ResponseMessage
                    {
                        Object = null,
                        ServiceMessage = new ServiceMessage
                        {
                            OperationSuccess = false,
                            ErrorMessage = message
                        }
                    };
                    return Ok(response);
                }

                var userResult = await UserTreatments("TakePhoto");

                if (userResult != null && !userResult.ServiceMessage.OperationSuccess)
                {
                    return userResult;
                }

                //INIT INTERVENTION : 
                var intervention = (await _iCVIService.GetInterventionByID(viewModel.InterventionId)).Item1;

  
                if (intervention.MutualisationPoint == null)
                {
                    message = $"Le point de mutualisation de l'intervention est vide";
                    _logger.Error($"POST /TakePhoto - error : {message}");

                    response = new ResponseMessage
                    {
                        Object = null,
                        ServiceMessage = new ServiceMessage
                        {
                            OperationSuccess = false,
                            ErrorMessage = message
                        }
                    };
                    return Ok(response);
                }

                var refServiceResult = await _referentialService.GetByPm(new Service.Referential.Request.GetByPmRequest
                {
                    PmName = intervention.MutualisationPoint.PMName
                });
                if (refServiceResult == null || !refServiceResult.Any())
                {
                    message = $"Pas de client Bouygues identifié sur ce PM";
                    _logger.Error($"POST /TakePhoto - error : {message}");

                    response = new ResponseMessage
                    {
                        Object = null,
                        ServiceMessage = new ServiceMessage
                        {
                            OperationSuccess = false,
                            ErrorMessage = message
                        }
                    };
                    return Ok(response);
                }

                //AlarmSystem
                var alarmServiceResult = await _alarmService.GetAllFakeAlarms(new GetAllFakeAlarmsRequest { ReferentialModel = refServiceResult });
                if (alarmServiceResult.Item2 != null)
                {
                    _logger.Error($"POST /TakePhoto - error : {alarmServiceResult.Item2.ErrorMessage}");

                    response = new ResponseMessage
                    {
                        Object = null,
                        ServiceMessage = alarmServiceResult.Item2
                    };
                    return Ok(response);
                }

                //CREATE PHOTO : 
                var resultCreatePhoto = await _iCVIService.ManagePhoto(new ManagePhotoRequest
                {
                    Action = Service.CVI.Request.enums.Action.Create,
                    Photo = new PhotoDTO
                    {
                        CreationDate = DateTime.Now,
                        Description = viewModel.Description,
                        Intervention = intervention,
                        User = userResult?.Object as UserDTO
                    }
                });
                if (resultCreatePhoto.Item2 == null && resultCreatePhoto.Item1 == null)
                {
                    message = $"Aucune photo n'a été ajouté";
                    _logger.Error($"POST /TakePhoto - error : {message}");

                    response = new ResponseMessage
                    {
                        Object = null,
                        ServiceMessage = new ServiceMessage
                        {
                            OperationSuccess = false,
                            ErrorMessage = message
                        }
                    };
                    return Ok(response);
                }
                else if (resultCreatePhoto.Item2 != null && resultCreatePhoto.Item2?.OperationSuccess == false)
                {
                    message = resultCreatePhoto.Item2.ErrorMessage;
                    _logger.Error($"POST /TakePhoto - error : {message}");

                    response = new ResponseMessage
                    {
                        Object = null,
                        ServiceMessage = new ServiceMessage
                        {
                            OperationSuccess = false,
                            ErrorMessage = message
                        }
                    };
                    return Ok(response);
                }

                var result = await _iCVIService.SavePhotoResultAndAlarms(new FillPhotoResultWithAlarmRequest
                {
                    Alarms = alarmServiceResult.Item1,
                    PhotoDTO = resultCreatePhoto.Item1,
                    RefResult = refServiceResult
                });
                if (result.Item2 == null && result.Item1 == null)
                {
                    message = $"Impossible de crée les alarms.";
                    _logger.Error($"POST /TakePhoto - error : {message}");

                    response = new ResponseMessage
                    {
                        Object = null,
                        ServiceMessage = new ServiceMessage
                        {
                            OperationSuccess = false,
                            ErrorMessage = message
                        }
                    };
                    return Ok(response);
                }
                else if (result.Item2 != null && !result.Item2.OperationSuccess)
                {
                    message = result.Item2.ErrorMessage;
                    _logger.Error($"POST /TakePhoto - error : {message}");

                    response = new ResponseMessage
                    {
                        Object = null,
                        ServiceMessage = new ServiceMessage
                        {
                            OperationSuccess = false,
                            ErrorMessage = message
                        }
                    };
                    return Ok(response);
                }

                return new ResponseMessage(result.Item1, message, true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return BadRequest(new ResponseMessage(null, ex.Message, false));
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ResponseMessage>> UpdateClientResult(UpdateClientResultViewModel viewModel)
        {
            try
            {
                ResponseMessage response;
                string message;

                if (viewModel == null)
                {
                    message = $"L'object est invalide.";
                    _logger.Error($"POST /UpdateClientResult - error : {message}");

                    response = new ResponseMessage
                    {
                        Object = null,
                        ServiceMessage = new ServiceMessage
                        {
                            OperationSuccess = false,
                            ErrorMessage = message
                        }
                    };
                    return Ok(response);
                }

                message = CheckUpdateClientResultRequired(viewModel);
                if (message != null)
                {
                    response = new ResponseMessage
                    {
                        Object = null,
                        ServiceMessage = new ServiceMessage
                        {
                            OperationSuccess = false,
                            ErrorMessage = message
                        }
                    };
                    return Ok(response);
                }

                //CREATE CLIENT RESULT : 
                var result = await _iCVIService.ManageClientResult(new ManageRequest()
                {
                    Action = Service.CVI.Request.enums.Action.Update,
                    Object = new ClientResultDto()
                    {
                        ID = viewModel.ClientResultId,
                        SrvID = viewModel.SrvID,
                        ONTPath = viewModel.ONTPath,
                        ConclusionId = viewModel.ConclusionId,
                        FirstCallComent = viewModel.FirstCallComment,
                        StatusFirstCallId = viewModel.StatusFirstCallId,
                        LocalizationId = viewModel.LocalizationId,
                        SubCauseId = viewModel.SubCauseId,
                        ProblemOrigin = viewModel.AdditionalInfo
                    }
                });
                return new ResponseMessage(result.Item1, message, true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return BadRequest(new ResponseMessage(null, ex.Message, false));
            }
        }

        /// <summary>
        /// Gets a collection of operator.
        /// </summary>
        /// <response code="200">Returns list of Ticket</response>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        [Route("{interventionId}")]
        public async Task<ActionResult<ResponseMessage>> GetClientsKO(int interventionId)
        {
            try
            {
                string message;
                if (interventionId < 1)
                {
                    message = $"L'id de la photo est obligatoire";
                    _logger.Error($"POST /GetClientsKO - error : {message}");

                    return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
                }

                (IEnumerable<ClientResultDto>, ServiceMessage) result = await _iCVIService.GetClientsKoByIntervention(interventionId);

                return new ResponseMessage(result.Item1, null, true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return new ResponseMessage(null, ex.Message, true);
            }
        }

        /// <summary>
        /// Gets photo by intervention id
        /// </summary>
        /// <response code="200">Returns list of Ticket</response>
        [HttpGet]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        [Route("{interventionId}")]
        public async Task<ActionResult<ResponseMessage>> GetPhotosByIntervention(int interventionId)
        {
            try
            {
                string message;

                message = CheckGetPhotoRequired(interventionId);
                if (message != null)
                {
                    return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
                }
                var result = await _iCVIService.GetPhotosByIntervention(new GetPhotosByInterventionRequest
                {
                    InterventionId = interventionId
                });

                return new ResponseMessage(result.Item1, null, true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return new ResponseMessage(null, ex.Message, false);
            }
        }

        /// <summary>
        /// Test alarmingSystem API
        /// </summary>
        /// <response code="200">Returns list of Ticket</response>
        [HttpPost]
        [ProducesResponseType(typeof(GetAllAlarmResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetAllAlarmResult>> GetAllAlarms()
        {
            var result = await _referentialService.GetByPm(new Service.Referential.Request.GetByPmRequest()
            {
                PmName = "FI-78490-000X"//intervention.PMName
            });

            var address = result.FirstOrDefault()?.PonPath;

            var alarmResult = await _alarmService.GetAllAlarms(new GetAllAlarmsRequest()
            {
                Asc = "true",
                Filter = new Filter()
                {
                    Address = address,
                    AlarmType = "INACT",
                },
                Size = "200",
                SortField = "startTime",
                StartIndex = "1"
            });

            return Ok(alarmResult);
        }

        /// <summary>
        /// Gets Code Decharge By Intervention Id.
        /// </summary>
        /// <response code="200">Returns Code Decharge</response>
        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<string>> GetCodeDechargeByInterventionIdAsync(int interventionId)
        {
            string message;
            if (interventionId <= 0)
            {
                message = $"L'id est invalide.";
                _logger.Error($"GET /GetCodeDechargeByInterventionIdAsync - error : {message}");
                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            //FIND Intervention : 
            var intervention = await _iCVIService.GetInterventionByID(interventionId);

            if (intervention.Item2 == null && intervention.Item1 == null)
            {
                message = $"L'intervention est introuvable.";
                _logger.Error($"GET /GetCodeDechargeByInterventionIdAsync - error : {message}");

                return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            if (intervention.Item2 != null && intervention.Item2?.OperationSuccess == false)
            {
                message = intervention.Item2.ErrorMessage;
                _logger.Error($"GET /GetCodeDechargeByInterventionIdAsync - error : {message}");

                return NotFound(new ErrorMessage { Code = "BadRequest", Message = message });
            }

            if (intervention.Item1.UnloadedCode == null)
            {
                string newCode = _iCVIService.GenerateCodeDecharge(intervention.Item1.BeginDate);
                var result = await _iCVIService.ManageIntervention(new ManageInterventionRequest
                {
                    Action = Service.CVI.Request.enums.Action.Update,
                    Intervention = new InterventionDTO
                    {
                        InterventionId = intervention.Item1.InterventionId,
                        InterventionPto = intervention.Item1.InterventionPto,
                        BeginDate = intervention.Item1.BeginDate,
                        CreationDate = intervention.Item1.CreationDate,
                        Name = intervention.Item1.Name,
                        UnloadedCode = newCode,
                        MutualisationPoint = intervention.Item1.MutualisationPoint,
                        Operator = intervention.Item1.Operator,
                        Type = intervention.Item1.Type,
                        User = intervention.Item1.User
                    }
                });

                if (result.Item2 == null || result.Item1 == null)
                {
                    message = $"Impossible d'jouter intervention.";
                    _logger.Error($"POST /UpdateIntervention - error : {message}");

                    return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
                }

                if (result.Item2 != null && result.Item2?.OperationSuccess == false)
                {
                    message = result.Item2.ErrorMessage;
                    _logger.Error($"POST /UpdateIntervention - error : {message}");

                    return BadRequest(new ErrorMessage { Code = "BadRequest", Message = message });
                }

                return newCode;
            }

            return intervention.Item1.UnloadedCode;
        }

        /// <summary>
        /// Get historic intervention
        /// </summary>
        /// <param name="interventionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{interventionId}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseMessage>> GetInterventionHistoric(int interventionId)
        {
            try
            {
                ResponseMessage response;
                var result = await _iCVIService.GetInterventionHistoric(new GetInterventionHistoricRequest
                {
                    InterventionId = interventionId
                });

                if (result.Item2 == null && result.Item1 == null)
                {
                    response = new ResponseMessage
                    {
                        Object = null,
                        ServiceMessage = new ServiceMessage
                        {
                            OperationSuccess = false,
                            ErrorMessage = $"Erreur sur GetInterventionHistoric service."
                        }
                    };
                    _logger.Error($"GET /GetInterventionHistoric - Erreur sur GetInterventionHistoric service.");

                    return Ok(response);
                }

                if (result.Item2 != null && result.Item2?.OperationSuccess == false)
                {
                    response = new ResponseMessage
                    {
                        Object = null,
                        ServiceMessage = new ServiceMessage
                        {
                            OperationSuccess = false,
                            ErrorMessage = $"{result.Item2.ErrorMessage}"
                        }
                    };
                    _logger.Error($"GET /GetInterventionHistoric - {result.Item2.ErrorMessage}.");

                    return Ok(response);
                }

                response = new ResponseMessage
                {
                    Object = result.Item1,
                    ServiceMessage = new ServiceMessage
                    {
                        OperationSuccess = true
                    }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Get alarms historic for photo
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{photoId}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseMessage>> GetHistoricAlarms(int photoId)
        {
            ResponseMessage response;
            var result = await _iCVIService.GetHistoricAlarms(new GetHistoricAlarmRequest
            {
                PhotoId = photoId
            });

            if (result.Item2 == null && result.Item1 == null)
            {
                response = new ResponseMessage
                {
                    Object = null,
                    ServiceMessage = new ServiceMessage
                    {
                        OperationSuccess = false,
                        ErrorMessage = $"Erreur sur GetHistoricAlarms service."
                    }
                };
                _logger.Error($"GET /GetHistoricAlarms - Erreur sur GetHistoricAlarms service.");

                return Ok(response);
            }

            if (result.Item2 != null && result.Item2?.OperationSuccess == false)
            {
                response = new ResponseMessage
                {
                    Object = null,
                    ServiceMessage = new ServiceMessage
                    {
                        OperationSuccess = false,
                        ErrorMessage = $"{result.Item2.ErrorMessage}."
                    }
                };
                _logger.Error($"GET /GetHistoricAlarms - {result.Item2.ErrorMessage}.");

                return Ok(response);
            }

            response = new ResponseMessage
            {
                Object = result.Item1,
                ServiceMessage = new ServiceMessage
                {
                    OperationSuccess = true
                }
            };

            return Ok(response);
        }

        /// <summary>
        /// Export Interventions Data
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{fromDate}/{toDate}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseMessage>> ExportInterventions(DateTime fromDate, DateTime toDate)
        {
            ResponseMessage response;
            var result = await _iCVIService.ExportInterventions(new ExportInterventionsRequest
            {
                FromDate = fromDate,
                ToDate = toDate
            });

            if (result.Item1 == null && result.Item2 == null && result.Item3 == null)
            {
                response = new ResponseMessage
                {
                    Object = null,
                    ServiceMessage = new ServiceMessage
                    {
                        OperationSuccess = false,
                        ErrorMessage = $"Erreur sur ExportInterventions service."
                    }
                };
                _logger.Error($"GET /ExportInterventions - ExportInterventions service Error");

                return Ok(response);
            }

            if (result.Item4 != null && result.Item4?.OperationSuccess == false)
            {
                response = new ResponseMessage
                {
                    Object = null,
                    ServiceMessage = new ServiceMessage
                    {
                        OperationSuccess = false,
                        ErrorMessage = $"{result.Item4.ErrorMessage}"
                    }
                };
                _logger.Error($"GET /GetHistoricAlarms - {result.Item4.ErrorMessage}.");

                return Ok(response);
            }

            ExportViewModel exportView = new ExportViewModel
            {
                Interventions = result.Item1.GroupBy(i => new { i.InterventionNum }).Select(i => i.First()).ToList(),
                ImpactedClients = result.Item2.GroupBy(i => new { i.InterventionNum, i.PhotoDate, i.SrvID, i.AlarmId }).Select(i => i.First()).ToList(),
                KoClients = result.Item3.GroupBy(i => new { i.InterventionNum, i.SrvID }).Select(i => i.First()).ToList()
            };

            response = new ResponseMessage
            {
                ServiceMessage = new ServiceMessage { OperationSuccess = true },
                Object = exportView
            };

            return Ok(response);

        }

        #region private validation methods


        private async Task<ResponseMessage> UserTreatments(string methodName)
        {
            string message;
            ResponseMessage response;
            //FIND CVI USER:
            UserDTO user = new UserDTO
            {
                LastName = "FIT DEV",
                FirstName = "BOUYGUES TELECOM",
                Login = "fitdev",
                Profil = "Administrateur",
                UserId = 1
            };
            var userResult = await _iCVIService.GetUser(new GetUserRequest
            {
                UserName = user.Login
            });
            if (userResult.Item2 == null && userResult.Item1 == null)
            {
                message = $"GetUser retour est nulle.";
                _logger.Error($"POST /{methodName} - error : {message}");

                response = new ResponseMessage
                {
                    Object = null,
                    ServiceMessage = new ServiceMessage
                    {
                        OperationSuccess = false,
                        ErrorMessage = message
                    }
                };
                return response;
            }
            else if (userResult.Item2 != null && userResult.Item2?.OperationSuccess == false)
            {
                message = userResult.Item2.ErrorMessage;
                _logger.Error($"POST /{methodName} - error : {message}");

                response = new ResponseMessage
                {
                    Object = null,
                    ServiceMessage = new ServiceMessage
                    {
                        OperationSuccess = false,
                        ErrorMessage = message
                    }
                };
                return response;
            }

            //CREATE NEW USER : 
            if (userResult.Item1 == null && userResult.Item2?.OperationSuccess == true)
            {
                userResult = await _iCVIService.ManageUser(new ManageRequest
                {
                    Action = Service.CVI.Request.enums.Action.Create,
                    Object = new UserDTO
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Login = user.Login,
                        Profil = user.Profil
                    }
                });

                if (userResult.Item2 == null && userResult.Item1 == null)
                {
                    message = $"Impossible de crée l'utilisateur.";
                    _logger.Error($"POST /{methodName} - error : {message}");

                    response = new ResponseMessage
                    {
                        Object = null,
                        ServiceMessage = new ServiceMessage
                        {
                            OperationSuccess = false,
                            ErrorMessage = message
                        }
                    };
                    return response;
                }

                if (userResult.Item2 != null && userResult.Item2?.OperationSuccess == false)
                {
                    message = userResult.Item2.ErrorMessage;
                    _logger.Error($"POST /{methodName} - error : {message}");

                    response = new ResponseMessage
                    {
                        Object = null,
                        ServiceMessage = new ServiceMessage
                        {
                            OperationSuccess = false,
                            ErrorMessage = message
                        }
                    };
                    return response;
                }
            }
            return new ResponseMessage(userResult, null, true);

        }
        /// <summary>
        /// Return type response from action controller
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        private ActionResult<ResponseMessage> ReturnActionController<T>((T, ServiceMessage) result, string methodName)
        {
            ResponseMessage response;
            string message;

            if (result.Item2 == null && result.Item1 == null)
            {
                message = $"Une erreur s'est produite.";
                _logger.Error($"POST /{methodName} - error : {message}");

                response = new ResponseMessage
                {
                    Object = null,
                    ServiceMessage = new ServiceMessage
                    {
                        OperationSuccess = false,
                        ErrorMessage = message
                    }
                };
                return Ok(response);
            }

            if (result.Item2?.OperationSuccess == false)
            {
                message = result.Item2.ErrorMessage;
                _logger.Error($"POST /{methodName} - error : {message}");

                response = new ResponseMessage
                {
                    Object = null,
                    ServiceMessage = new ServiceMessage
                    {
                        OperationSuccess = false,
                        ErrorMessage = message
                    }
                };
                return Ok(response);
            }

            return new ResponseMessage(result.Item1, null, true);
        }


        /// <summary>
        /// The private check required method
        /// </summary>
        /// <returns>The message</returns>
        private string CheckCreateInterventionRequired(InterventionViewModel interventionViewModel)
        {
            string message;
            if (string.IsNullOrWhiteSpace(interventionViewModel.Name))
            {
                message = $"Le nom est obligatoire.";
                _logger.Error($"POST / CreateIntervention - error : {message}");
                return message;
            }

            if (interventionViewModel.PmID <= 0)
            {
                message = $"Le PMName est obligatoire.";
                _logger.Error($"POST / CreateIntervention - error : {message}");
                return message;
            }

            if (interventionViewModel.TypeId <= 0)
            {
                message = $"Le TypeId est obligatoire.";
                _logger.Error($"POST / CreateIntervention - error : {message}");
                return message;
            }

            if (interventionViewModel.OperatorId <= 0)
            {
                message = $"Le OperatorId est obligatoire.";
                _logger.Error($"POST / CreateIntervention - error : {message}");
                return message;
            }

            return null;
        }

        /// <summary>
        /// The private check required method
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private string CheckTakePhotoRequired(TakePhotoViewModel viewModel)
        {
            string message;
            if (viewModel.InterventionId <= 0)
            {
                message = $"Le paramètre InterventionId est obligatoire";
                _logger.Error($"POST / TakePhoto - error : {message}");
                return message;
            }

            return null;
        }

        /// <summary>
        /// The private check required method
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private string CheckUpdateClientResultRequired(UpdateClientResultViewModel viewModel)
        {
            string message;
            if (viewModel.ClientResultId <= 0)
            {
                message = $"Le client est obligatoire";
                _logger.Error($"POST / TakePhoto - error : {message}");
                return message;
            }

            return null;
        }


        /// <summary>
        /// The private check required method
        /// </summary>
        /// <param name="interventionId"></param>
        /// <returns></returns>
        private string CheckGetPhotoRequired(int interventionId)
        {
            string message;
            if (interventionId <= 0)
            {
                message = $"Le paramètre InterventionId est obligatoire.";
                _logger.Error($"POST / getPhotosByIntervention - error : {message}");
                return message;
            }
            return null;
        }

        #endregion

    }
}
