using CVI.Domain.Model;
using CVI.Domain.Model.Intervention;
using CVI.Domain.Commun;
using CVI.Service.CVI.Assembler;
using CVI.Service.CVI.Contract;
using CVI.Service.CVI.DTO;
using CVI.Service.CVI.DTO.Intervention;
using CVI.Service.CVI.Request;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVI.Domain.Model.Photo;
using CVI.Service.CVI.DTO.Photo;
using CVI.Service.Authent.Contract;
using CVI.Service.Authent.Request;
using CVI.Service.Referential.Result;
using CVI.Service.AlarmSystem.ItemData;
using log4net;
using CVI.Domain.Model.Enums;
using CVI.Service.CVI.DTO.Histo;
using CVI.Service.CVI.DTO.Export;
using CVI.Domain.Common;
using System.Security.Cryptography;

namespace CVI.Service.CVI
{
    /// <summary>
    /// The CVIService class
    /// </summary>
    public class CviService : ICviService
    {
        #region PROPS

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog _logger = LogManager.GetLogger(typeof(CviService));

        /// <summary>
        /// Authentication service
        /// </summary>
        private readonly IAuthenticationService _authenticationService;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public CviService(IUnitOfWork unitOfWork, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region INTERVENTION

        /// <summary>
        /// Get all interventions
        /// </summary>
        /// <returns></returns>
        public async Task<(IEnumerable<InterventionDTO>, ServiceMessage)> GetAllInterventions()
        {
            ServiceMessage serviceMessage = new ServiceMessage
            {
                OperationSuccess = true
            };

            var intrs = await _unitOfWork.InterventionRepository.FindAll(i => i.InterventionType,
                i => i.Operator, i => i.User, i => i.MutualisationPoint);
            IEnumerable<InterventionDTO> interventions = intrs.Select(i => i.ToInterventionDto())
                .AsEnumerable();
            return (interventions, serviceMessage);
        }

        /// <summary>
        /// Manage intervention
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(InterventionDTO, ServiceMessage)> ManageIntervention(ManageInterventionRequest request)
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

            if (request.Intervention == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Intervention est vide.",
                    OperationSuccess = false
                };
                _logger.Error("The intervetionDto object is null.");
                return (null, serviceMessage);
            }

            Intervention inrtToManage = request.Intervention.ToIntervention();

            //Get intervention type by Id
            var type = (await _unitOfWork.InterventionTypeRepository.Find(t => t.ID == inrtToManage.InterventionType.ID)).FirstOrDefault();
            //Get operator by Id
            var @operator = (await _unitOfWork.OperatorRepository.Find(t => t.ID == inrtToManage.Operator.ID)).FirstOrDefault();
            //Get user by Id
            var user = (await _unitOfWork.UserRepository.Find(t => t.ID == inrtToManage.User.ID)).FirstOrDefault();
            //Get mutualisation point by Id
            var pm = (await _unitOfWork.MutualisationPointRepository.Find(t => t.ID == inrtToManage.MutualisationPoint.ID)).FirstOrDefault();

            inrtToManage.InterventionType = type;
            inrtToManage.MutualisationPoint = pm;
            inrtToManage.Operator = @operator;
            inrtToManage.User = user;

            Intervention intervention = new Intervention();

            switch (request.Action)
            {
                case Request.enums.Action.Create:

                    intervention = await _unitOfWork.InterventionRepository.CreateAsync(inrtToManage);
                    break;
                case Request.enums.Action.Update:
                    intervention = await _unitOfWork.InterventionRepository.UpdateAsync(inrtToManage, inrtToManage.ID);
                    break;
                case Request.enums.Action.Delete:
                    await _unitOfWork.InterventionRepository.DeleteAsync(inrtToManage);
                    break;
                default:
                    break;
            }
            await _unitOfWork.SaveAsync();

            serviceMessage = new ServiceMessage { OperationSuccess = true };
            return (intervention.ToInterventionDto(), serviceMessage);

        }

        /// <summary>
        /// Get intervention By ID
        /// </summary>
        /// <returns></returns>
        public async Task<(InterventionDTO, ServiceMessage)> GetInterventionByID(int interventionId)
        {
            ServiceMessage serviceMessage = new ServiceMessage
            {
                OperationSuccess = true
            };

            var intrs = await _unitOfWork.InterventionRepository.Find(i => i.ID == interventionId, i => i.InterventionType,
                i => i.Operator, i => i.User, i => i.MutualisationPoint);
            InterventionDTO interventions = intrs.Select(i => i.ToInterventionDto()).FirstOrDefault();
            return (interventions, serviceMessage);
        }

        /// <summary>
        /// Get intervention By Name
        /// </summary>
        /// <returns></returns>
        public async Task<(InterventionDTO, ServiceMessage)> GetInterventionByName(GetInterventionByNameRequest request)
        {

            var intervention = await _unitOfWork.InterventionRepository.First(i => i.InterventionName == request.Name, i => i.InterventionType,
                i => i.Operator, i => i.User, i => i.MutualisationPoint);

            if (intervention != null)
            {
                ServiceMessage serviceMessage = new ServiceMessage
                {
                    OperationSuccess = true
                };
                InterventionDTO interventions = intervention.ToInterventionDto();
                return (interventions, serviceMessage);
            }
            else
            {
                ServiceMessage serviceMessage = new ServiceMessage
                {
                    OperationSuccess = false
                };
                return (null, serviceMessage);
            }
        }

        /// <summary>
        /// Get intervention type By Name
        /// </summary>
        /// <returns></returns>
        public async Task<(InterventionTypeDTO, ServiceMessage)> GetInterventionTypeByName(GetInterventionTypeByNameRequest request)
        {

            var interventionType = await _unitOfWork.InterventionTypeRepository.First(i => i.InterventionTypeName == request.TypeName);

            if (interventionType != null)
            {
                ServiceMessage serviceMessage = new ServiceMessage
                {
                    OperationSuccess = true
                };
                InterventionTypeDTO interventionDTO = interventionType.ToInterventionTypeDto();
                return (interventionDTO, serviceMessage);
            }
            else
            {
                ServiceMessage serviceMessage = new ServiceMessage
                {
                    OperationSuccess = false
                };
                return (null, serviceMessage);
            }
        }

        /// <summary>
        /// Generate Code Décharge
        /// </summary>
        /// <returns></returns>
        public string GenerateCodeDecharge(DateTime? dateTime)
        {

            int d = int.Parse(DateTime.Today.ToString("yyyyMMdd"));
            int randNumber = RandomNumberGenerator.GetInt32(21000, 200000);
            string first = (d / randNumber).ToString();

            string begin = dateTime?.ToString("yyyyMMddhhmmss");
            if (begin != null)
            {
                string second = "" + begin[RandomNumberGenerator.GetInt32(3)] + begin[RandomNumberGenerator.GetInt32(3, 8)] + begin[RandomNumberGenerator.GetInt32(8, 13)];

                int t = int.Parse(DateTime.Today.ToString("hhmmss"));
                int randNumber2 = RandomNumberGenerator.GetInt32(700, 5500);
                int r = (t * 4) / randNumber2;
                string third = r < 100 ? "0" + r : "" + r;


                return first + "-" + second + "-" + third;
            }
            else return "";
            
        }

        #endregion

        #region INTERVENTION TYPE

        /// <summary>
        /// Find all intervention type
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<InterventionTypeDTO>, ServiceMessage)> GetAllInterventionTypes()
        {
            IEnumerable<InterventionType> types = (await _unitOfWork.InterventionTypeRepository.FindAll()).AsEnumerable();
            return (types?.Select(t => t.ToInterventionTypeDto()).AsEnumerable(), new ServiceMessage { OperationSuccess = true });
        }

        /// <summary>
        /// Find the intervention type
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(InterventionTypeDTO, ServiceMessage)> GetInterventionType(GetInterventionTypeRequest request)
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

            if (request.TypeId <= 0)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "TypeId est vide.",
                    OperationSuccess = false
                };
                _logger.Error("The Id object is null.");
                return (null, serviceMessage);
            }

            InterventionType interventionType = (await _unitOfWork.InterventionTypeRepository.Find(t => t.ID == request.TypeId)).FirstOrDefault();
            return (interventionType?.ToInterventionTypeDto(), new ServiceMessage { OperationSuccess = true });
        }

        #endregion

        #region OPERATOR

        /// <summary>
        /// Get operator by Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(OperatorDTO, ServiceMessage)> GetOperator(GetOperatorRequest request)
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

            if (request.OperatorId <= 0)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "OperatorId est vide.",
                    OperationSuccess = false
                };
                _logger.Error("The Id object is null.");
                return (null, serviceMessage);
            }

            Operator oprt = (await _unitOfWork.OperatorRepository.Find(t => t.ID == request.OperatorId)).FirstOrDefault();
            return (oprt?.ToOperatorDto(), new ServiceMessage { OperationSuccess = true });
        }

        /// <summary>
        /// Get operator by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<(OperatorDTO, ServiceMessage)> GetOperatorByName(GetOperatorByNameRequest request)
        {
            ServiceMessage serviceMessage;

            try
            {
                if (string.IsNullOrEmpty(request.Name))
                {
                    serviceMessage = new ServiceMessage
                    {
                        ErrorMessage = "Name est vide.",
                        OperationSuccess = false
                    };
                    _logger.Error("The Name is null or empty.");
                    return (null, serviceMessage);
                }

                Operator op = (await _unitOfWork.OperatorRepository.Find(x => x.OperatorName.Trim().ToUpper() == request.Name.Trim().ToUpper())).FirstOrDefault();
                if (op != null)
                {
                    return (op.ToOperatorDto(), new ServiceMessage { OperationSuccess = true });
                }
                else
                {
                    return (null, new ServiceMessage { OperationSuccess = false, ErrorMessage = $"No Operator find for name '{request.Name}'" });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return (null, null);
            }
        }

        /// <summary>
        /// Get the last intervention created for PmName
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(InterventionDTO, ServiceMessage)> GetLastInterventionByPmName(GetLastInterventionByPmNameRequest request)
        {
            ServiceMessage serviceMessage;

            try
            {
                if (string.IsNullOrEmpty(request.Name))
                {
                    serviceMessage = new ServiceMessage
                    {
                        ErrorMessage = "Name est vide.",
                        OperationSuccess = false
                    };
                    _logger.Error("The Name is null or empty.");
                    return (null, serviceMessage);
                }

                Intervention intervention = (await _unitOfWork.InterventionRepository.Find(x => x.MutualisationPoint.PMName.Trim().ToUpper() == request.Name.Trim().ToUpper())).OrderByDescending(x => x.CreationDate).FirstOrDefault();
                if (intervention != null)
                {
                    return (intervention.ToInterventionDto(), new ServiceMessage { OperationSuccess = true });
                }
                else
                {
                    return (null, new ServiceMessage { OperationSuccess = true });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return (null, null);
            }
        }

        /// <summary>
        /// Find all operators
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<OperatorDTO>, ServiceMessage)> GetAllOperators()
        {
            IEnumerable<Operator> oprts = (await _unitOfWork.OperatorRepository.FindAll()).AsEnumerable();
            return (oprts?.Select(o => o.ToOperatorDto()).AsEnumerable(), new ServiceMessage { OperationSuccess = true });
        }

        #endregion

        #region CVI USERS

        /// <summary>
        /// Get user by username
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(UserDTO, ServiceMessage)> GetUser(GetUserRequest request)
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

            if (string.IsNullOrWhiteSpace(request.UserName))
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "le login est vide.",
                    OperationSuccess = false
                };
                _logger.Error("The user name is null.");
                return (null, serviceMessage);
            }

            User user = (await _unitOfWork.UserRepository.First(t => t.Login == request.UserName));
            return (user?.ToUserDto(), new ServiceMessage { OperationSuccess = true });
        }

        /// <summary>
        /// Manage User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(UserDTO, ServiceMessage)> ManageUser(ManageRequest request)
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
                    ErrorMessage = "User est vide.",
                    OperationSuccess = false
                };
                _logger.Error("The userDTO object is null.");
                return (null, serviceMessage);
            }

            if (request.Object.GetType() != typeof(UserDTO))
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Type request type n'est pas valide.",
                    OperationSuccess = false
                };
                _logger.Error("The UserDTO object is null.");
                return (null, serviceMessage);
            }

            UserDTO user = request.Object as UserDTO;
            User userToManage = user?.ToUser();
            
            User result = new User();

            //SEARCH USER : 
            if (request.Action == Request.enums.Action.Update)
            {
                var upResult = (await _unitOfWork.UserRepository.First(t => t.Login == user.Login));

                if (upResult == null)
                {
                    serviceMessage = new ServiceMessage
                    {
                        ErrorMessage = $"Utilisateur n'est pas trouvé avec Login { user.Login }.",
                        OperationSuccess = false
                    };
                    _logger.Error($"No user object with the login { user.Login }.");
                    return (null, serviceMessage);
                }
                
                userToManage = upResult;  
            }

            switch (request.Action)
            {
                case Request.enums.Action.Create:
                    result = await _unitOfWork.UserRepository.CreateAsync(userToManage);
                    break;
                case Request.enums.Action.Update:
                    if(userToManage != null)
                    {
                        userToManage.FirstName = user.FirstName;
                        userToManage.LastName = user.LastName;
                        userToManage.Profil = user.Profil;

                        result = await _unitOfWork.UserRepository.UpdateAsync(userToManage, userToManage.ID);
                    }
                    
                    break;
                case Request.enums.Action.Delete:
                    await _unitOfWork.UserRepository.DeleteAsync(userToManage);
                    break;
                default:
                    break;
            }

            await _unitOfWork.SaveAsync();
            serviceMessage = new ServiceMessage { OperationSuccess = true };
            return (result.ToUserDto(), serviceMessage);
        }

        #endregion

        #region PHOTO

        /// <summary>
        /// ManagePhoto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(PhotoDTO, ServiceMessage)> ManagePhoto(ManagePhotoRequest request)
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

            if (request.Photo == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Photo est vide.",
                    OperationSuccess = false
                };
                _logger.Error("The Photo object is null.");
                return (null, serviceMessage);
            }

            if (request.Photo.Intervention == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Numéro d'intervention est vide.",
                    OperationSuccess = false
                };
                _logger.Error("The intervetionDto object is null.");
                return (null, serviceMessage);
            }

            if (request.Photo.User == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "User id est vide.",
                    OperationSuccess = false
                };
                _logger.Error("The UserDto object is null.");
                return (null, serviceMessage);
            }

            //SEARCH PHOTO : 
            if (request.Action == Request.enums.Action.Update)
            {
                var photo = (await _unitOfWork.PhotoRepository.Find(i => i.ID == request.Photo.ID)).FirstOrDefault();
                if (photo == null)
                {
                    serviceMessage = new ServiceMessage
                    {
                        ErrorMessage = $"Pas de photo avec ID {request.Photo.ID}.",
                        OperationSuccess = false
                    };

                    _logger.Error("The photo object is not found.");
                    return (null, serviceMessage);
                }
            }

            //SEARCH INTERVENTION : 
            var inter = (await _unitOfWork.InterventionRepository.First(i => i.ID == request.Photo.Intervention.InterventionId, i => i.MutualisationPoint));
            if (inter == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = $"Pas de intervention avec ID {request.Photo.Intervention.InterventionId}.",
                    OperationSuccess = false
                };

                _logger.Error("The intervetionDto object is not found.");
                return (null, serviceMessage);
            }

            //SEARCH USER : 
            var user = (await _unitOfWork.UserRepository.Find(i => i.ID == request.Photo.User.UserId)).FirstOrDefault();
            if (user == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = $"Pas de user avec ID {request.Photo.Intervention.InterventionId}.",
                    OperationSuccess = false
                };

                _logger.Error("The user object is not found.");
                return (null, serviceMessage);
            }

            //SEARCH OR CREATE PHOTO RESULT : 
            PhotoResult photoResult;

            //if no photoResult found
            if (request.Photo.PhotoResult == null || request.Photo.PhotoResult.ID < 1)
            {
                photoResult = new PhotoResult { ClientNumber = 0, DownClientNumber = 0, NOKClientNumber = 0, CreationDate = DateTime.Now };
                _unitOfWork.PhotoResultRepository.Attach(photoResult);
                await _unitOfWork.PhotoResultRepository.CreateAsync(photoResult);
            }
            else
            {
                photoResult = await _unitOfWork.PhotoResultRepository.First(c => c.ID == request.Photo.PhotoResult.ID);
            }

            //TO DO : ALARM ET PHOTO RESULT
            Photo managedObject = new Photo
            {
                CreationDate = request.Photo.CreationDate,
                Description = request.Photo.Description,
                ID = request.Photo.ID,
                Intervention = inter,
                User = user,
                PhotoResult = photoResult
            };

            Photo result = new Photo();
            switch (request.Action)
            {
                case Request.enums.Action.Create:
                    result = await _unitOfWork.PhotoRepository.CreateAsync(managedObject);
                    break;
                case Request.enums.Action.Update:
                    result = await _unitOfWork.PhotoRepository.UpdateAsync(managedObject, managedObject.ID);
                    break;
                case Request.enums.Action.Delete:
                    await _unitOfWork.PhotoRepository.DeleteAsync(managedObject);
                    break;
                default:
                    break;
            }
            await _unitOfWork.SaveAsync();

            //SET NAVIGATION OBJECTS : 
            result.Intervention = inter;

            serviceMessage = new ServiceMessage { OperationSuccess = true };
            return (result?.ToPhotoDto(), serviceMessage);
        }

        /// <summary>
        /// Get photos by intervention Id
        /// </summary>
        /// <param name="interventionId"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<PhotoDTO>, ServiceMessage)> GetPhotosByIntervention(GetPhotosByInterventionRequest request)
        {
            ServiceMessage serviceMessage;
            IEnumerable<PhotoDTO> photos;


            if (request.InterventionId < 1)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "InterventionId is null",
                    OperationSuccess = false
                };

                _logger.Error("The request is null.");
                return (null, serviceMessage);
            }

            photos = (await _unitOfWork.PhotoRepository.Find(x => x.Intervention.ID == request.InterventionId, x => x.Intervention, x => x.PhotoResult, x => x.User))
                .Select(x => x.ToPhotoDto()).AsEnumerable();

            return (photos, new ServiceMessage { OperationSuccess = true });
        }
        /// <summary>
        /// Fill photo results with alarm results
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(PhotoDTO, ServiceMessage)> SavePhotoResultAndAlarms(FillPhotoResultWithAlarmRequest request)
        {
            ServiceMessage serviceMessage;

            DateTime now = DateTime.Now;

            if (request.PhotoDTO?.Intervention?.MutualisationPoint == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Le point de mutualisation de l'intervention est vide.",
                    OperationSuccess = false
                };

                _logger.Error("The PM request is null.");
                return (null, serviceMessage);
            }

            if (request.RefResult == null || !request.RefResult.Any())
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Pas de client Bouygues identifié sur ce PM",
                    OperationSuccess = false
                };

                _logger.Error("The request is null.");
                return (null, serviceMessage);
            }
            var address = request.RefResult.FirstOrDefault()?.PonPath;

            if (string.IsNullOrEmpty(address))
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Pas de client Bouygues identifié sur ce PM",
                    OperationSuccess = false
                };
                _logger.Error($"The request is null : Le resultat de l'API REF ne contient pas de PON PATH : {serviceMessage.ErrorMessage}");
                return (null, serviceMessage);
            }

            if (request.Alarms == null || !request.Alarms.Any())
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Aucune alarme enregistrée sur la période de cette intervention",
                    OperationSuccess = false
                };
                _logger.Error($"NOTIFICTION API : No alarms from notification API {serviceMessage.ErrorMessage}");
            }

            var photos = (await _unitOfWork.PhotoRepository.Find(p => p.InterventionId == request.PhotoDTO.Intervention.InterventionId, p => p.Intervention, p => p.PhotoResult)).OrderByDescending(p => p.CreationDate);
            var isFirstPhoto = photos.Count() == 1; // si c'est la première photo sur cette intervention
            var photo = photos.FirstOrDefault();

            //Récupère la liste des clients déja détectés sur cette interventions
            var oldClient = (await _unitOfWork.AlarmRepository.Find(a => a.ClientResult.InterventionId == request.PhotoDTO.Intervention.InterventionId,
                a => a.ClientResult)).Select(x => x.ClientResult.SrvID).Distinct();

            //status par défaut
            var statusFirstCallDefault = (await _unitOfWork.StatusClientRepository.First(x => x.IsDefault));
            //conclusion init
            var conclusionNouveauKO = await _unitOfWork.ConclusionRepository.First(x => x.Name == "Nouveau voisin KO");

            //FILTRATION ALARM
            var newAlarmsForIntervention = request.PhotoDTO.Intervention.BeginDate.HasValue ?
                request.Alarms.Where(a => a.StartDateTime >= request.PhotoDTO.Intervention.BeginDate) :
                request.Alarms.Where(a => a.StartDateTime >= request.PhotoDTO.Intervention.CreationDate);


            //SAVE ALARMS AND CLIENT RESULT
            List<Alarm> alarmsToAdd = new List<Alarm>();

            await SaveAlarmsAndClientResultForPhoto(new SaveAlarmsForPhotoRequest 
            { 
                PhotoDTO = request.PhotoDTO,
                ResultReferential = request.RefResult, 
                DateNow = now, 
                AlarmsToAdd = alarmsToAdd, 
                IsFirstPhoto = isFirstPhoto, 
                StatusFirstCallDefault = statusFirstCallDefault, 
                ConclusionNouveauKO = conclusionNouveauKO, 
                NewAlarmsForIntervention = newAlarmsForIntervention 
            });

            await _unitOfWork.AlarmRepository.CreateRangeAsync(alarmsToAdd);
            await _unitOfWork.SaveAsync();



            //SAVE PHOTORESULT
            var photoResult = photo.PhotoResult;

            photoResult.ClientNumber = request.RefResult.Select(x => x.SrvId).Distinct().Count();
            photoResult.CreationDate = now;
            photoResult.DownClientNumber = alarmsToAdd.Where(x => x.ClearTime.HasValue && !oldClient.Any(oa => oa == x.ClientResult.SrvID))
                .Select(x => x.ClientResult.SrvID).Distinct().Count();
            photoResult.NOKClientNumber = alarmsToAdd.Where(x => !x.ClearTime.HasValue).Select(x => x.ClientResult.SrvID).Distinct().Count();
            var DGOFF = request.RefResult.Where(c => c.LinkStatus == "SWITCHED_OFF");
            photoResult.SwitchedOffNumber = DGOFF.Count();
            photoResult.ListDGOFF = string.Join("|", DGOFF.Select(c => c.SrvId));
            _unitOfWork.PhotoResultRepository.Attach(photoResult);
            photoResult = await _unitOfWork.PhotoResultRepository.UpdateAsync(photoResult, photoResult.ID);
            await _unitOfWork.SaveAsync();


            //MAJ PHOTO
            photo.Alarms = alarmsToAdd;
            photo.PhotoResult = photoResult;

            var result = (await _unitOfWork.PhotoRepository.UpdateAsync(photo, photo.ID));

            await _unitOfWork.SaveAsync();

            return (result.ToPhotoDto(), null);
        }

        /// <summary>
        /// Save alarms for photo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task SaveAlarmsAndClientResultForPhoto(SaveAlarmsForPhotoRequest request)
        {
            //Grouper la liste d'alarms par Num ONT et Address pour parcourir
            foreach (var alarmGroup in request.NewAlarmsForIntervention.GroupBy(a => new { a.ONTNr, a.PonPath }))
            {
                //la liste d'alarme pour le client courant
                var alarms = alarmGroup.ToList();

                //l'item qui matche avec le num ONT et l'adress
                var item = request.ResultReferential.FirstOrDefault(x => x.NumONT == alarmGroup.Key.ONTNr && x.PonPath == alarmGroup.Key.PonPath);
                if (item != null)
                {
                    //récupérer le client result de la base s'il existe
                    ClientResult clientResultEntity = await _unitOfWork.ClientResultRepository.First(x => x.SrvID == item.SrvId && x.InterventionId == request.PhotoDTO.Intervention.InterventionId);

                    //S'il n'existe pas on le créer
                    if (clientResultEntity == null)
                    {
                        clientResultEntity = await CreateClientResult(request.PhotoDTO, request.DateNow, request.StatusFirstCallDefault, alarms, item, request.IsFirstPhoto, request.ConclusionNouveauKO);
                    }
                    //sinon on met à jour ses valeurs IsDown et ClientStep
                    else
                    {
                        clientResultEntity = await UpdateClientResult(clientResultEntity, alarms);
                    }

                    //Remplir la liste des nouvelles alarms à ajouter
                    request.AlarmsToAdd.AddRange(alarms.Select(a => AttachNewAlarm(request.DateNow, a, clientResultEntity, request.PhotoDTO.ID)));
                }

            }
        }

        /// <summary>
        /// Attach the new alarm to add
        /// </summary>
        /// <param name="now"></param>
        /// <param name="alarm"></param>
        /// <param name="clientResult"></param>
        /// <param name="photoId"></param>
        /// <returns></returns>
        private Alarm AttachNewAlarm(DateTime now, AlarmItemData alarm, ClientResult clientResult, int photoId)
        {
            var newAlarm = new Alarm
            {
                Address = alarm.Address,
                AlarmType = alarm.AlarmType,
                ClearTime = alarm.ClearDateTime,
                CreationDate = now,
                NotificationId = alarm.NotificationId,
                PhotoId = photoId,
                ONTNr = alarm.ONTNr,
                StartTime = alarm.StartDateTime.Value,
                ClientResult = clientResult
            };

            _unitOfWork.AlarmRepository.Attach(newAlarm);

            return newAlarm;
        }

        #endregion

        #region RESULT CLIENT       

        /// <summary>
        /// Récupère tous les clients KOs d'une intervention
        /// </summary>
        /// <param name="interventionId"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<ClientResultDto>, ServiceMessage)> GetClientsKoByIntervention(int interventionId)
        {
            ServiceMessage serviceMessage;

            if (interventionId < 1)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "l'ID de l'intervention est obligatoire",
                    OperationSuccess = false
                };

                _logger.Error("l'ID de l'intervention est obligatoire");
                return (null, serviceMessage);
            }

            //FIND INTERVENTION
            var intervention = (await _unitOfWork.InterventionRepository.First(p => p.ID == interventionId, p => p.Photos));

            if (intervention == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "l'intervention est est introuvable",
                    OperationSuccess = false
                };

                _logger.Error("l'intervention est introuvable");
                return (null, serviceMessage);
            }

            //GET CLIENT RESULT
            var result = (await _unitOfWork.AlarmRepository.Find(a => !a.ClearTime.HasValue && a.ClientResult.InterventionId == interventionId, a => a.ClientResult))
                .Select(a => a.ClientResult).Distinct()
                .Select(cr => cr.ToClientResultDto());

            return (result, null);

        }

        /// <summary>
        /// Manage Client result
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(ClientResultDto, ServiceMessage)> ManageClientResult(ManageRequest request)
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
                    ErrorMessage = "Object est vide.",
                    OperationSuccess = false
                };
                _logger.Error("The DTO object is null.");
                return (null, serviceMessage);
            }

            if (request.Object.GetType() != typeof(ClientResultDto))
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Type request type n'est pas valide.",
                    OperationSuccess = false
                };
                _logger.Error("The DTO object type is not valide.");
                return (null, serviceMessage);
            }


            ClientResultDto clientResult = request.Object as ClientResultDto;
            ClientResult objToManage = clientResult?.ToClientResult();

            //FIND CLIENT RESULT WHEN UPDATE :
            if (request.Action == Request.enums.Action.Update)
            {
                objToManage = (await _unitOfWork.ClientResultRepository.Find(cr => cr.ID == objToManage.ID)).FirstOrDefault();
                if (objToManage == null)
                {
                    serviceMessage = new ServiceMessage
                    {
                        ErrorMessage = $"Pas de client result trouvé avec ID { clientResult?.ID }.",
                        OperationSuccess = false
                    };
                    _logger.Error($"No ClientResult object found with ID { clientResult?.ID }.");
                    return (null, serviceMessage);
                }
            }


            //SET CLIENT RESULT OBJECT : 
            if(clientResult != null)
            {
                objToManage.SrvID = clientResult.SrvID;
                objToManage.ONTPath = clientResult.ONTPath;
                objToManage.LocalizationId = clientResult.LocalizationId;
                objToManage.StatusFirstCallId = clientResult.StatusFirstCallId;
                objToManage.ConclusionId = clientResult.ConclusionId;
                objToManage.SubCauseId = clientResult.SubCauseId;
                objToManage.AdditionalInfos = clientResult.ProblemOrigin;
                objToManage.FirstCallComent = clientResult.FirstCallComent;
            }
            

            ClientResult result = new ClientResult();

            switch (request.Action)
            {
                case Request.enums.Action.Create:
                    result = await _unitOfWork.ClientResultRepository.CreateAsync(objToManage);
                    break;
                case Request.enums.Action.Update:
                    result = await _unitOfWork.ClientResultRepository.UpdateAsync(objToManage, objToManage.ID);
                    break;
                case Request.enums.Action.Delete:
                    await _unitOfWork.ClientResultRepository.DeleteAsync(objToManage);
                    break;
                default:
                    break;
            }
            await _unitOfWork.SaveAsync();
            serviceMessage = new ServiceMessage
            {
                OperationSuccess = true
            };
            return (result?.ToClientResultDto(), serviceMessage);

        }

        /// <summary>
        /// Ajouter un nouveau client Result en base avec des valeurs par défaut
        /// </summary>
        /// <param name="photoDTO"></param>
        /// <param name="now"></param>
        /// <param name="statusFirstCallDefault"></param>
        /// <param name="alarms"></param>
        /// <param name="refItem"></param>
        /// <param name="isFirstPhoto"></param>
        /// <param name="conclusionNouveauKO"></param>
        /// <returns></returns>
        private async Task<ClientResult> CreateClientResult(PhotoDTO photoDTO, DateTime now, StatusFirstCall statusFirstCallDefault, List<AlarmItemData> alarms, ReferentialModel refItem, bool isFirstPhoto, Conclusion conclusionNouveauKO)
        {
            var isDown = alarms.Any(a => !a.ClearDateTime.HasValue);
            Conclusion conclusion1erCheckKO = !isFirstPhoto && isDown ? conclusionNouveauKO : null;

            var clientResultEntity = new ClientResult
            {
                CreationDate = now,
                StatusFirstCall = statusFirstCallDefault,
                InterventionId = photoDTO.Intervention.InterventionId,
                IsDown = isDown,
                ConclusionId = conclusion1erCheckKO?.ID,
                AdditionalInfos = conclusion1erCheckKO?.Name,
                ClientStep = ClientSteps.newKO,
                ONTPath = refItem.ONTPath,
                SrvID = refItem.SrvId,
                IdOSS = refItem.IdOSS
            };

            _unitOfWork.ClientResultRepository.Attach(clientResultEntity);

            await _unitOfWork.ClientResultRepository.CreateAsync(clientResultEntity);
            return clientResultEntity;
        }
        
        /// <summary>
        /// Mettre à jour les valeurs IsDown et ClientStep du client grace à la liste d'alarmes
        /// </summary>
        /// <param name="clientResult"></param>
        /// <param name="alarms"></param>
        /// <returns></returns>
        private async Task<ClientResult> UpdateClientResult(ClientResult clientResult, List<AlarmItemData> alarms)
        {
            var isDown = alarms.Any(a => !a.ClearDateTime.HasValue);

            clientResult.IsDown = isDown;
            clientResult.ClientStep = isDown ? ClientSteps.oldKO : ClientSteps.oldOK;
            await _unitOfWork.ClientResultRepository.UpdateAsync(clientResult, clientResult.ID);

            return clientResult;
        }

        #endregion

        #region AUTHENT

        /// <summary>
        /// Récupérer l'utilisateur connecté
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(UserDTO, ServiceMessage)> GetLoggedUser(GetCVILoggedUserRequest request)
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
            if (string.IsNullOrWhiteSpace(request.UserName))
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "UserName is empty.",
                    OperationSuccess = false
                };
                _logger.Error($"UserName is empty : {serviceMessage.ErrorMessage}");
                return (null, serviceMessage);
            }
            if (string.IsNullOrWhiteSpace(request.AppID))
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "APPID n'est pas valide.",
                    OperationSuccess = false
                };
                _logger.Error($"APPID not valid: {serviceMessage.ErrorMessage}");
                return (null, serviceMessage);
            }

            try
            {
                var result = await _authenticationService.GetUser(new GetLoggedUserRequest
                {
                    Login = request.UserName,
                    AppID = request.AppID
                });


                if (result.Item2 == null || result.Item1 == null)
                {
                    serviceMessage = new ServiceMessage
                    {
                        ErrorMessage = "Utilisateur est introuvable.",
                        OperationSuccess = false
                    };
                    _logger.Error($"_authenticationService return null : {serviceMessage.ErrorMessage}");
                    return (null, serviceMessage);
                }

                if (result.Item2 != null && result.Item2?.OperationSuccess == false)
                {
                    serviceMessage = new ServiceMessage
                    {
                        ErrorMessage = result.Item2?.ErrorMessage,
                        OperationSuccess = false
                    };
                    _logger.Error($"_authenticationService return null : {serviceMessage.ErrorMessage}");
                    return (null, serviceMessage);
                }

                //ANNUAIRE FIT INFOS : 
                if (string.IsNullOrWhiteSpace(result.Item1.Profil))
                {
                    serviceMessage = new ServiceMessage
                    {
                        ErrorMessage = "Profil is empty.",
                        OperationSuccess = false
                    };
                    _logger.Error($"The request is null : {serviceMessage.ErrorMessage}");
                    return (new UserDTO
                    {
                        FirstName = result.Item1.FirstName,
                        LastName = result.Item1.LastName,
                        Profil = result.Item1.Profil,
                        Login = request.UserName
                    }, serviceMessage);
                }

                if (string.IsNullOrWhiteSpace(result.Item1.LastName) || string.IsNullOrWhiteSpace(result.Item1.FirstName))
                {
                    _logger.Error($"Authent service => First name : {result.Item1.FirstName} last name : {result.Item1.LastName} ");
                }

                return (new UserDTO
                {
                    FirstName = result.Item1.FirstName,
                    LastName = result.Item1.LastName,
                    Profil = result.Item1.Profil,
                    Login = request.UserName
                }, result.Item2);
            }
            catch (Exception ex)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = ex.Message,
                    OperationSuccess = false
                };
                _logger.Error(ex.Message, ex);
                return (null, serviceMessage);
            }
        }

        #endregion

        #region MUTUALIZATION POINT

        /// <summary>
        /// Manage Mutualization Point
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(MutualisationPointDTO, ServiceMessage)> ManageMutualisationPoint(ManageRequest request)
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
                    ErrorMessage = "PM est vide.",
                    OperationSuccess = false
                };
                _logger.Error("The PM object is null.");
                return (null, serviceMessage);
            }

            if (request.Object.GetType() != typeof(MutualisationPointDTO))
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Type request type n'est pas valide.",
                    OperationSuccess = false
                };
                _logger.Error("The MutualisationPointDto object is not valide.");
                return (null, serviceMessage);
            }

            MutualisationPointDTO pm = request.Object as MutualisationPointDTO;

            MutualisationPoint objToManage = pm?.ToMutualisationPoint();
            MutualisationPoint result = new MutualisationPoint();

            switch (request.Action)
            {
                case Request.enums.Action.Create:
                    result = await _unitOfWork.MutualisationPointRepository.CreateAsync(objToManage);
                    break;
                case Request.enums.Action.Update:
                    var attached = (await _unitOfWork.MutualisationPointRepository.Find(l => l.ID == objToManage.ID)).FirstOrDefault();
                    attached.PMName = objToManage.PMName;
                    result = await _unitOfWork.MutualisationPointRepository.UpdateAsync(attached, attached.ID);
                    break;
                case Request.enums.Action.Delete:
                    await _unitOfWork.MutualisationPointRepository.DeleteAsync(objToManage);
                    break;
                default:
                    break;
            }
            await _unitOfWork.SaveAsync();
            serviceMessage = new ServiceMessage { OperationSuccess = true };
            return (result?.ToMutualisationPointDto(), serviceMessage);
        }

        /// <summary>
        /// get Mutualization Point by id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(MutualisationPointDTO, ServiceMessage)> GetMutualisationPoint(GetMutualisationPointRequest request)
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

            if (request.PmId <= 0)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "PM ID est vide.",
                    OperationSuccess = false
                };
                _logger.Error("The PM ID is null.");
                return (null, serviceMessage);
            }

            MutualisationPoint pm = (await _unitOfWork.MutualisationPointRepository.Find(pm => pm.ID == request.PmId)).FirstOrDefault();
            return (pm.ToMutualisationPointDto(), new ServiceMessage { OperationSuccess = true });
        }

        /// <summary>
        /// Get All Mutualization Points
        /// </summary>
        /// <returns></returns>
        public async Task<(IEnumerable<MutualisationPointDTO>, ServiceMessage)> GetAllMutualisationPoints()
        {
            IEnumerable<MutualisationPoint> pms = (await _unitOfWork.MutualisationPointRepository.FindAll()).AsEnumerable();
            return (pms?.Select(t => t.ToMutualisationPointDto()).AsEnumerable(), new ServiceMessage { OperationSuccess = true });
        }

        /// <summary>
        /// Get mutualization point by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<(MutualisationPointDTO, ServiceMessage)> GetMutualisationPointbyName(string name)
        {
            ServiceMessage serviceMessage;

            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    serviceMessage = new ServiceMessage
                    {
                        ErrorMessage = "PM Name est vide.",
                        OperationSuccess = false
                    };
                    _logger.Error("The PM Name is null or empty.");
                    return (null, serviceMessage);
                }

                MutualisationPoint pm = (await _unitOfWork.MutualisationPointRepository.Find(x => x.PMName.Trim().ToUpper() == name.Trim().ToUpper())).FirstOrDefault();
                if (pm != null)
                {
                    return (pm.ToMutualisationPointDto(), new ServiceMessage { OperationSuccess = true });
                }
                else
                {
                    return (null, new ServiceMessage { OperationSuccess = false, ErrorMessage = $"No PM find for name '{name}'" });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return (null, null);
            }
        }

        /// <summary>
        /// Get Intervention by Pm
        /// </summary>
        /// <param name="request">the request to find intervention</param>
        /// <returns></returns>
        public async Task<(IEnumerable<InterventionDTO>, ServiceMessage)> GetInterventionByPm(GetInterventionByPmRequest request)
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

            if (request.PmID <= 0)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "PmID est vide.",
                    OperationSuccess = false
                };
                _logger.Error("The  PmID is null.");
                return (null, serviceMessage);
            }

            //FIND INTERVENTION : 
            MutualisationPoint pm = (await _unitOfWork.MutualisationPointRepository.Find(i => i.ID == request.PmID)).FirstOrDefault();

            if (pm == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = $"PM not found with ID { request.PmID}",
                    OperationSuccess = false
                };
                _logger.Error($"PM not found with ID { request.PmID}");
                return (null, serviceMessage);
            }

            IEnumerable<Intervention> interventions = (await _unitOfWork.InterventionRepository.Find(pm => pm.MutualisationPoint.ID == request.PmID, i => i.MutualisationPoint)).ToList();
            return (interventions.Select(i => i.ToInterventionDto()).ToList(), new ServiceMessage { OperationSuccess = true });
        }

        #endregion

        #region HISTORIC

        /// <summary>
        /// Get Intervention Historic
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(InterventionHistoricDTO, ServiceMessage)> GetInterventionHistoric(GetInterventionHistoricRequest request)
        {
            ServiceMessage serviceMessage;

            if (request == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "GetInterventionHistoric : Param est pas valide.",
                    OperationSuccess = false
                };
                _logger.Error("GetInterventionHistoric : The request is null.");
                return (null, serviceMessage);
            }

            if (request.InterventionId < 1)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "GetInterventionHistoric : Id n'est pas valide.",
                    OperationSuccess = false
                };
                _logger.Error($"GetInterventionHistoric: ID is null.");
                return (null, serviceMessage);
            }

            //SEARCH INTERVENTION : 
            var intervention = await _unitOfWork.InterventionRepository.First(i => i.ID == request.InterventionId,
                i => i.User,
                i => i.Operator,
                i => i.InterventionType,
                i => i.MutualisationPoint);

            if (intervention == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = $"Pas d'intervention pour l'Id {request.InterventionId}.",
                    OperationSuccess = false
                };
                _logger.Error($"GetInterventionHistoric: No intervention with the given ID {request.InterventionId}.");
                return (null, serviceMessage);
            }

            //SEARCH PHOTOS : 
            var photos = (await _unitOfWork.PhotoRepository.Find(
                p => p.InterventionId == request.InterventionId,
                p => p.PhotoResult,
                p => p.User)
             ).ToList();

            if (photos == null || photos.Count == 0)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = $"Pas de photos pour cette intervention.",
                    OperationSuccess = false
                };
                _logger.Error($"GetInterventionHistoric: No photos for this intervention.");
                return (null, serviceMessage);
            }



            //TOTAL CLIENT client au PM
            var clientCount = photos.Max(p => p.PhotoResult?.ClientNumber);


            //SEARCH ALARMS : 
            List<HistoPhotoDTO> phs = new List<HistoPhotoDTO>();
            List<Alarm> alarms = new List<Alarm>();

            foreach (var photo in photos)
            {
                var alrs = await _unitOfWork.AlarmRepository.Find(a => a.PhotoId == photo.ID,
                    a => a.ClientResult,
                    a => a.ClientResult.Conclusion,
                    a => a.ClientResult.StatusFirstCall,
                    a => a.ClientResult.Localization,
                    a => a.ClientResult.Localization,
                    a => a.ClientResult.SubCause);
                alarms.AddRange(alrs);

                var impactedClientPhoto = photo.PhotoResult?.DownClientNumber;
                var KoClient = photo.PhotoResult?.NOKClientNumber;

                phs.Add(new HistoPhotoDTO
                {
                    CreationDate = photo.CreationDate,
                    Description = photo.Description,
                    User = photo.User?.ToUserDto(),
                    PhotoID = photo.ID,
                    ImpactedClient = impactedClientPhoto ?? 0,
                    KoClient = KoClient ?? 0
                });
            }

            if (alarms == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Erreur pendant la récupération des alarmes",
                    OperationSuccess = false
                };
                _logger.Error($"GetInterventionHistoric: Alarms list is null.");
                return (null, serviceMessage);
            }

            //IMPACTED CLIENTS : 
            var impactedClient = photos.Sum(p => p.PhotoResult.DownClientNumber);

            //ALARM <20
            var impactedAlarms = alarms.Where(a => a.StartTime.HasValue && a.ClearTime.HasValue);

            var alarmsCutOffInf20 = impactedAlarms.Where(a => (a.ClearTime - a.StartTime).Value.TotalSeconds < 20).Select(x => x.NotificationId).Distinct();

            //ALARM >=20
            var alarmsCutOffSup20 = impactedAlarms.Where(a => (a.ClearTime - a.StartTime).Value.TotalSeconds >= 20).Select(x => x.NotificationId).Distinct();

            //TimingCutAvg:
            double deltaSum = impactedAlarms.Select(x => new { x.NotificationId, x.StartTime, x.ClearTime }).Distinct().Sum(a => (a.ClearTime - a.StartTime).Value.TotalSeconds);

            var TimingCutAvg = (impactedAlarms.Any()) ? deltaSum / impactedAlarms.Select(x => x.NotificationId).Distinct().Count() : 0;

            //CLIENTS KO
            var clientsKo = alarms.Where(a => !a.ClearTime.HasValue).Select(a => a.ClientResult.SrvID).Distinct();

            //CLIENT RESULT
            var clientsResult = alarms.Where(a => a.ClearTime == null).Select(a => a.ClientResult).Distinct();

            //LAST PHOTO : 
            DateTime lastDate = photos.Max(p => p.CreationDate);
            var lastPhoto = photos.FirstOrDefault(p => p.CreationDate == lastDate);

            //GLOBAL INFO :
            InterventionGlobalInfoDTO globalInfo = new InterventionGlobalInfoDTO
            {
                //InterventionStatus = !lastPhoto?.Alarms?.Any(a => !a.ClearTime.HasValue) == null ? true : !lastPhoto.Alarms.Any(a => !a.ClearTime.HasValue),
                InterventionStatus = (lastPhoto != null && !lastPhoto.Alarms.Any(a => !a.ClearTime.HasValue)) || lastPhoto == null,
                IsClientKo = clientsKo.Any(),
                ImpactedClientCount = impactedClient,
                PmCount = clientCount ?? 0,
                Inferior20CutCount = alarmsCutOffInf20.Count(),
                Superior20CutCount = alarmsCutOffSup20.Count(),
                TimingCutAvg = TimingCutAvg
            };

            InterventionHistoricDTO interventionHistoric = new InterventionHistoricDTO
            {
                Intervention = new InterventionDTO
                {
                    BeginDate = intervention.BeginDate,
                    CreationDate = intervention.CreationDate,
                    InterventionId = intervention.ID,
                    InterventionPto = intervention.InterventionPto,
                    Name = intervention.InterventionName,
                    UnloadedCode = intervention.UnloadedCode,
                    MutualisationPoint = intervention.MutualisationPoint?.ToMutualisationPointDto(),
                    Type = intervention.InterventionType?.ToInterventionTypeDto(),
                    Operator = intervention.Operator?.ToOperatorDto()
                },
                GlobalInfo = globalInfo,
                Photos = phs,
                KoClients = clientsResult?.Select(cr => new HistoClientDTO
                {
                    ClientStatus = cr?.StatusFirstCall?.Name,
                    Localization = cr?.Localization?.Name,
                    Info = cr?.AdditionalInfos,
                    SrvID = cr?.SrvID,
                    SubCause = cr?.SubCause?.Name
                }).Distinct()
            };

            //RETURN
            serviceMessage = new ServiceMessage { OperationSuccess = true };
            return (interventionHistoric, serviceMessage);
        }

        /// <summary>
        /// Get delta alarm using photo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<HistoAlarmDto>, ServiceMessage)> GetHistoricAlarms(GetHistoricAlarmRequest request)
        {
            ServiceMessage serviceMessage;

            if (request == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "GetHistoricAlarm : Param est pas valide.",
                    OperationSuccess = false
                };
                _logger.Error("GetHistoricAlarm : The request is null.");
                return (null, serviceMessage);
            }

            if (request.PhotoId < 1)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "GetHistoricAlarm : l'Id n'est pas valide.",
                    OperationSuccess = false
                };
                _logger.Error($"GetHistoricAlarm: ID is null.");
                return (null, serviceMessage);
            }

            //SEARCH PHOTO : 
            Photo photo = await _unitOfWork.PhotoRepository.First(i => i.ID == request.PhotoId);

            if (photo == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = $"Pas de photo pour cet identifiant Id {request.PhotoId}.",
                    OperationSuccess = false
                };
                _logger.Error($"GetHistoricAlarm: No photo with the given ID {request.PhotoId}.");
                return (null, serviceMessage);
            }

            //ALARM : 
            List<Alarm> alarmsEntities = await _unitOfWork.AlarmRepository.Find(i => i.PhotoId == request.PhotoId, i => i.ClientResult);
            IEnumerable<HistoAlarmDto> alarms = alarmsEntities.Select(a => new HistoAlarmDto
            {
                Address = a.Address,
                AlarmType = a.AlarmType,
                ClearTime = a.ClearTime,
                NotificationId = a.NotificationId,
                ONTNr = a.ONTNr,
                StartTime = a.StartTime,
                SrvID = a.ClientResult.SrvID
            });
            if (alarms == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Erreur lors de la récupération des alarmes",
                    OperationSuccess = false
                };
                _logger.Error($"GetHistoricAlarm: Error while getting alarms.");
                return (null, serviceMessage);
            }

            //FIND INTERVENTION
            Intervention intervention = await _unitOfWork.InterventionRepository.First(i => i.ID == photo.InterventionId, i => i.Photos);
            Photo prevPhoto = intervention.Photos
                .Where(p => p.CreationDate < photo.CreationDate)
                .OrderBy(o => o.CreationDate)
                .LastOrDefault();



            IEnumerable<HistoAlarmDto> alarmsDto = new List<HistoAlarmDto>();
            if (prevPhoto == null)
            {
                //RETURN
                serviceMessage = new ServiceMessage { OperationSuccess = true };
                return (alarms, serviceMessage);
            }
            else
            {
                IEnumerable<Alarm> prevAlarms = await _unitOfWork.AlarmRepository.Find(a => a.PhotoId == prevPhoto.ID);
                if (prevAlarms == null || !prevAlarms.Any())
                {
                    //RETURN
                    serviceMessage = new ServiceMessage { OperationSuccess = true };
                    return (alarms, serviceMessage);
                }
                else
                {
                    IEnumerable<HistoAlarmDto> prevAlarmsDto = prevAlarms.Select(a => new HistoAlarmDto
                    {
                        Address = a.Address,
                        AlarmType = a.AlarmType,
                        ClearTime = a.ClearTime,
                        NotificationId = a.NotificationId,
                        ONTNr = a.ONTNr,
                        StartTime = a.StartTime
                    });
                    alarmsDto = alarms.Where(a => !prevAlarmsDto.Any(pa => pa.NotificationId == a.NotificationId && pa.ClearTime == a.ClearTime && pa.Address == a.Address));
                }
            }
            //RETURN
            serviceMessage = new ServiceMessage { OperationSuccess = true };
            return (alarmsDto, serviceMessage);
        }

        #endregion

        #region EXPORT

        /// <summary>
        /// Export Interventions data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<ExportInterventionDTO>, IEnumerable<ExportImpactedClientDto>, List<ExportKoClientDto>, ServiceMessage)> ExportInterventions(ExportInterventionsRequest request)
        {
            ServiceMessage serviceMessage;

            if (request == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "ExportInterventions : Param est pas valide.",
                    OperationSuccess = false
                };
                _logger.Error("ExportInterventions : The request is null.");
                return (null, null, null, serviceMessage);
            }

            if (request.FromDate == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "ExportInterventions : FromDate est pas valide.",
                    OperationSuccess = false
                };
                _logger.Error("ExportInterventions : The FromDate is null.");
                return (null, null, null, serviceMessage);
            }

            if (request.ToDate == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "ExportInterventions : ToDate est pas valide.",
                    OperationSuccess = false
                };
                _logger.Error("ExportInterventions : The ToDate is null.");
                return (null, null, null, serviceMessage);
            }

            if (request.ToDate < request.FromDate)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "ExportInterventions : ToDate < FromDate.",
                    OperationSuccess = false
                };
                _logger.Error("ExportInterventions : ToDate < FromDate.");
                return (null, null, null, serviceMessage);
            }

            //SEARCH INTERVENTIONS : 
            var interventions = await _unitOfWork.InterventionRepository.Find(i => i.CreationDate.Date >= request.FromDate.Date
               && i.CreationDate.Date <= request.ToDate.Date,
                i => i.InterventionType,
                i => i.MutualisationPoint,
                i => i.Operator,
                i => i.User,
                i => i.Photos,
            i => i.InterventionType);

            List<ExportInterventionDTO> exportInterventions = new List<ExportInterventionDTO>();
            List<ExportImpactedClientDto> impactedClients = new List<ExportImpactedClientDto>();
            List<ExportKoClientDto> koClients = new List<ExportKoClientDto>();

            foreach (var intervention in interventions)
            {
                //PTO : 
                string interventionPto = string.Empty;
                if (!string.IsNullOrWhiteSpace(intervention.InterventionPto) && intervention.InterventionPto.Length >= 8)
                {
                    interventionPto =
                        string.Concat("FI-", intervention.InterventionPto.Substring(0, 4), "-", intervention.InterventionPto.Substring(4, 4));
                }

                var photos = (await _unitOfWork.PhotoRepository.Find(p => p.InterventionId == intervention.ID,
                    i => i.PhotoResult,
                    i => i.Alarms)).OrderBy(p => p.CreationDate);


                List<Alarm> alarms = new List<Alarm>();
                foreach (var photo in photos)
                {
                    alarms.AddRange(photo.Alarms);
                }

                var clientResults = await _unitOfWork.ClientResultRepository.Find(
                    cr => cr.InterventionId == intervention.ID,
                    cr => cr.StatusFirstCall,
                    cr => cr.Localization,
                    cr => cr.Conclusion,
                    cr => cr.SubCause);

                int totalClientsCount = photos.Max(p => p.PhotoResult?.ClientNumber).HasValue ? photos.Max(p => p.PhotoResult?.ClientNumber).Value : 0;

                //Statut first call : 
                var statusList = clientResults.Select(cr => cr.StatusFirstCall?.Name);

                //Localisation & conclusions : 
                var locConList = clientResults.Select(cr => string.Concat(cr.Localization?.Name, "-", cr.Conclusion?.Name));

                //RestoredClientsCountOfFirstPhoto : 
                var firstPhotoAlarms = alarms.Where(a => a.PhotoId == photos.FirstOrDefault()?.ID);
                var restoredClientsCountOfFirstPhoto =
                    firstPhotoAlarms?.Where(a => a.ClearTime != null)?.Select(a => a?.ClientResult?.SrvID)?.Distinct()?.Count();

                //RestoredClientsCountOfOthersPhotos
                var alarmOfFirstPhoto = alarms.Where(p => p.PhotoId == photos.FirstOrDefault().ID);
                var alarmsOfOthersPhotos = alarms.Where(a => a.PhotoId != photos.FirstOrDefault().ID &&
                    !alarmOfFirstPhoto.Any(aof => aof.NotificationId == a.NotificationId));
                var restoredClientsCountOfOthersPhotos =
                    alarmsOfOthersPhotos?.Where(a => a.ClearTime != null)?.Select(a => a?.ClientResult?.SrvID)?.Distinct()?.Count();

                //Alarm short cut
                var alarmShortCul = alarms.Where(a => a.ClearTime != null && (a.ClearTime - a.StartTime).Value.TotalSeconds < 20);
                var alarmGroupedByClient = alarmShortCul?.GroupBy(a => a?.ClientResult?.SrvID);
                var clientMoreThen5alarm = alarmGroupedByClient?.Where(g => g.Count() >= 5).Select(g => g.Key).Distinct();
                var clientLessThen5alarm = alarmGroupedByClient?.Where(g => g.Count() < 5).Select(g => g.Key).Distinct();

                //LAST PHOTO : 
                var lastPhoto = photos.OrderBy(p => p.CreationDate).LastOrDefault();

                //Not restored client : 
                var koClient = photos.OrderBy(p => p.CreationDate).LastOrDefault()?.Alarms?.Where(a => a.ClearTime == null)?.Select(a => a.ClientResult?.SrvID).Distinct();

                // ko clients in all but last photo
                var koClientAllButLastPhoto = alarms?.Where(a => a.ClearTime == null && a.PhotoId != lastPhoto.ID)?.Select(a => a.ClientResult?.SrvID).Distinct();

                //not KO at last photo
                var noKoClients = photos.OrderBy(p => p.CreationDate).LastOrDefault()?.Alarms?.Where(a => a.ClearTime != null)?.Select(a => a.ClientResult?.SrvID).Distinct();

                var restoredKOClients = noKoClients?.Join(koClientAllButLastPhoto, j => j, f => f, (j, f) => new { j }).Count();

                //Clients
                var clients = string.Join("|", clientResults?.Select(cr => cr.SrvID)?.Distinct().ToList());

                exportInterventions.Add(new ExportInterventionDTO
                {
                    InterventionType = intervention.InterventionType?.InterventionTypeName,
                    InterventionPm = intervention.MutualisationPoint?.PMName,
                    InterventionNum = intervention.InterventionName,
                    InterventionPto = interventionPto,
                    CreatedBy = intervention.User.Login,
                    StartDateIntervention = intervention.BeginDate.HasValue ? intervention.BeginDate.Value : intervention.CreationDate,
                    EndDateIntervention = photos.LastOrDefault()?.CreationDate,
                    Clients = clients,
                    TotalCliensCount = totalClientsCount,
                    RestoredClientsCountOfFirstPhoto = restoredClientsCountOfFirstPhoto.HasValue ?
                        restoredClientsCountOfFirstPhoto.Value : 0,
                    RestoredClientsCountOfOthersPhotos = restoredClientsCountOfOthersPhotos.HasValue ?
                        restoredClientsCountOfOthersPhotos.Value : 0,
                    RestoredClientsCountWithShortCutLessThen5Alarms = clientLessThen5alarm == null ? 0 : clientLessThen5alarm.Count(),
                    RestoredClientsCountWithShortCutWith5Alarms = clientMoreThen5alarm == null ? 0 : clientMoreThen5alarm.Count(),
                    NotRestoredCliensCount = koClient == null ? 0 : koClient.Count(),
                    RestoredKOClients = restoredKOClients,
                    DgClientCount = photos?.OrderBy(p => p.CreationDate).LastOrDefault()?.PhotoResult.SwitchedOffNumber ?? 0,
                    ListDGOFF = photos?.OrderBy(p => p.CreationDate).LastOrDefault()?.PhotoResult.ListDGOFF,
                    FirstCallStatus = string.Join("|", statusList),
                    ConclusionAndLocalisation = string.Join("|", locConList)
                });

                //IMPACTED CLIENTS :  
                foreach (var alarm in alarms.Where(a => a.ClearTime != null).OrderBy(a => a.Photo.CreationDate))
                {
                    var alarmsOfSrvId = alarms.Where(a => a.ClearTime != null
                       && a.ClientResult.SrvID == alarm.ClientResult.SrvID).Select(a => new { a.NotificationId, a.ONTNr, a.StartTime, a.ClearTime }).Distinct();

                    var durations = alarmsOfSrvId?.Select(a => a.ClearTime - a.StartTime);
                    var totalSpan = new TimeSpan(durations.Sum(r => r.Value.Ticks));

                    impactedClients.Add(new ExportImpactedClientDto
                    {
                        InterventionNum = intervention.InterventionName,
                        InterventionPm = intervention.MutualisationPoint?.PMName,
                        PhotoDate = alarm.Photo?.CreationDate,
                        PhotoUser = alarm?.Photo?.User?.Login,
                        PhotoDescription = alarm.Photo?.Description,
                        SrvID = (alarm?.ClientResult?.SrvID).HasValue ? (alarm?.ClientResult?.SrvID).Value : 0,
                        SumDurationOfInterruption = totalSpan.TotalSeconds,
                        AlarmId = alarm.NotificationId,
                        AlarmType = alarm.AlarmType,
                        AlarmAdress = alarm.Address,
                        AlarmOnt = alarm.ONTNr,
                        AlarmStartTime = alarm.StartTime,
                        AlarmEntTime = alarm.ClearTime
                    });
                }

                //KO CLIENTS :
                foreach (var alarm in alarms.Where(a => a.ClearTime == null).OrderBy(a => a.CreationDate))
                {
                    if (alarm.ClientResult != null)
                    {
                        koClients.Add(new ExportKoClientDto
                        {
                            InterventionNum = intervention.InterventionName,
                            InterventionPm = intervention.MutualisationPoint?.PMName,
                            SrvID = alarm.ClientResult.SrvID,
                            StartTimeAlarm = alarm.StartTime,
                            FinalStatus = lastPhoto.Alarms.Any(a => a.ClientResult.SrvID == alarm.ClientResult.SrvID && a.ClearTime != null),
                            FirstCallStatus = alarm.ClientResult?.StatusFirstCall?.Name,
                            FirstCallStatusComment = alarm.ClientResult?.FirstCallComent,
                            AdditionalInfo = alarm.ClientResult.AdditionalInfos,
                            Conclusion = alarm.ClientResult.Conclusion?.Name,
                            Location = alarm.ClientResult.Localization?.Name,
                            SubCause = alarm.ClientResult.SubCause?.Name
                        });
                    }
                }

            }

            return (exportInterventions, impactedClients, koClients, new ServiceMessage
            {
                OperationSuccess = true
            });
        }
        #endregion
    }
}
