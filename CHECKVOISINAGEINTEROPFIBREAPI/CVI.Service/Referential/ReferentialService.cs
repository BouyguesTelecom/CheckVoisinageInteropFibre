using CVI.Domain.Common;
using CVI.Service.Referential.Contract;
using CVI.Service.Referential.Request;
using CVI.Service.Referential.Result;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVI.Service.Referential
{
    /// <summary>
    /// The ReferentialService class
    /// </summary>
    public class ReferentialService : IReferentialService
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILog _logger = LogManager.GetLogger(typeof(ReferentialService));

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ReferentialService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ReferentialModel>> GetByPm(GetByPmRequest request)
        {
            try
            {
                var equipements  = await _unitOfWork.EquipementRepository.FindAll();
                IEnumerable<ReferentialModel> pms = equipements.Select(e => new ReferentialModel
                {
                    Date = e.Date,
                    IdOSS = e.IdOSS,
                    LinkStatus = e.LinkStatus,
                    NomNro = e.NomNro,
                    NomPm = e.NomPm,
                    NumONT = e.NumONT,
                    ONTPath = e.ONTPath, 
                    PonPath = e.PonPath,
                    SrvId = e.SrvId
                }); 
                return pms;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }
    }
}
