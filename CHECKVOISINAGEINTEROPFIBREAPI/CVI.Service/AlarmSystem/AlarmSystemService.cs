using CVI.Domain.Common;
using CVI.Domain.Commun;
using CVI.Service.AlarmSystem.Contract;
using CVI.Service.AlarmSystem.ItemData;
using CVI.Service.AlarmSystem.Request;
using CVI.Service.AlarmSystem.Result;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVI.Service.AlarmSystem
{
    /// <summary>
    /// The NAVService class
    /// </summary>
    public class AlarmSystemService : IAlarmSystemService
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILog _logger = LogManager.GetLogger(typeof(AlarmSystemService));

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public AlarmSystemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(IEnumerable<AlarmItemData>, ServiceMessage)> GetAllFakeAlarms(GetAllFakeAlarmsRequest request)
        {
            ServiceMessage serviceMessage = null;

            if (request == null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = $"GetAllAlarmsForCVI : L'objet request est null",
                    OperationSuccess = false
                };

                _logger.Error("request object is null");
                return (null, serviceMessage);
            }
            else if (request.ReferentialModel == null || !request.ReferentialModel.Any())
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = $"Pas de clients Bouygues identifiés sur ce PM",
                    OperationSuccess = false
                };

                _logger.Error("The referential had no result");
                return (null, serviceMessage);
            }

            //upgrade alarms date : 
            var notifications = await _unitOfWork.NotificationRepository.FindAll();
            bool isFirst = notifications.Exists(x => x.StartDateTime.ToString("yyyy-dd-MM") == "2021-08-03");
            var alarmsKO = await _unitOfWork.NotificationRepository.Find(a => a.ClearTime == null);
            var alarmsOk = await _unitOfWork.NotificationRepository.Find(a => a.ClearTime != null);
            var entDate = DateTime.Now.AddMinutes(2).AddSeconds(20);
            var startDate = DateTime.Now.AddMinutes(2);

            if (isFirst)
            {
                foreach (var alarm in alarmsKO)
                {
                    alarm.StartTime = startDate.ToString();
                    alarm.StartDateTime = startDate;
                    await _unitOfWork.NotificationRepository.UpdateAsync(alarm, alarm.ID);
                }
                foreach (var alarm in alarmsOk)
                {
                    alarm.StartTime = startDate.ToString();
                    alarm.ClearTime = entDate.ToString();
                    alarm.StartDateTime = startDate;
                    alarm.ClearDateTime = entDate;

                    await _unitOfWork.NotificationRepository.UpdateAsync(alarm, alarm.ID);
                }
                await _unitOfWork.SaveAsync();
            }
            else
            {
                foreach (var alarm in alarmsKO)
                {
                    alarm.ClearTime = entDate.ToString();
                    alarm.ClearDateTime = entDate;
                    await _unitOfWork.NotificationRepository.UpdateAsync(alarm, alarm.ID);
                }
                foreach (var alarm in alarmsOk)
                {
                    await _unitOfWork.NotificationRepository.DeleteAsync(alarm);
                }
                await _unitOfWork.SaveAsync();
            }

            var alarms = await _unitOfWork.NotificationRepository.FindAll();
            IEnumerable<AlarmItemData> result = alarms.Where(a => request.ReferentialModel
                .Select(r => r.PonPath)
                .Any(ad => ad == a.PonPath))
                .Select(a => new AlarmItemData
            {
                Address = a.Address,
                AlarmType = a.AlarmType,
                ClearTime = a.ClearTime,
                NotificationId = a.NotificationId,
                ONTNr = a.ONTNr,
                PonPath = a.PonPath,
                StartTime = a.StartTime
            });
            return (result, null);
        }

        public async Task<(GetAllAlarmResult, GetAllAlarmsListResult, ServiceMessage)> GetAllAlarms(GetAllAlarmsRequest request)
        {
            try
            {
                ServiceMessage serviceMessage;
                if (request is null)
                {
                    serviceMessage = new ServiceMessage
                    {
                        ErrorMessage = "La requête n'est pas valide",
                        OperationSuccess = false
                    };
                    _logger.Error("The request is null");
                    return (null, null, serviceMessage);
                }

                var nots = await _unitOfWork.NotificationRepository.FindAll();
                GetAllAlarmsListResult getAllAlarmsList = new GetAllAlarmsListResult
                {
                    GetAllAlarmsResponse = new GetAllAlarmsListResponse
                    {
                        Alarm = nots.Select(not => new AlarmItemData
                        {
                            Address = not.Address,
                            AlarmType = not.AlarmType,
                            ClearTime = not.ClearTime,
                            NotificationId = not.NotificationId,
                            StartTime = not.StartTime,
                            ONTNr = not.ONTNr
                        })
                    }
                };
                return (null, getAllAlarmsList, new ServiceMessage
                {
                    OperationSuccess = true
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return (null, null, new ServiceMessage
                {
                    OperationSuccess = false,
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
