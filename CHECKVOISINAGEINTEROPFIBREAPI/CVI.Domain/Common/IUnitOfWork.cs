using CVI.Domain.IRepository;
using CVI.Domain.Model;
using CVI.Domain.Model.Alarms;
using CVI.Domain.Model.Intervention;
using CVI.Domain.Model.Photo;
using CVI.Domain.Model.Referential;
using System.Threading.Tasks;

namespace CVI.Domain.Common
{
    /// <summary>
    /// The IUnitOfWork interface.
    /// </summary>
    public interface IUnitOfWork
    {
        #region REPOS

        /// <summary>
        /// Gets InterventionRepository
        /// </summary>
        public IGenericRepository<Intervention> InterventionRepository { get; }

        /// <summary>
        /// Gets InterventionTypeRepository
        /// </summary>
        public IGenericRepository<InterventionType> InterventionTypeRepository { get; }

        /// <summary>
        /// Gets PhotoRepository
        /// </summary>
        public IGenericRepository<Photo> PhotoRepository { get; }

        /// <summary>
        /// Gets PhotoResultRepository
        /// </summary>
        public IGenericRepository<PhotoResult> PhotoResultRepository { get; }

        /// <summary>
        /// Gets AlarmRepository
        /// </summary>
        public IGenericRepository<Alarm> AlarmRepository { get; }

        /// <summary>
        /// Gets OperatorRepository
        /// </summary>
        public IGenericRepository<Operator> OperatorRepository { get; }

        /// <summary>
        /// Gets RejectCauseRepository
        /// </summary>
        public IGenericRepository<ClientResult> RejectCauseRepository { get; }

        /// <summary>
        /// Gets UserRepository
        /// </summary>
        public IGenericRepository<User> UserRepository { get; }

        /// <summary>
        /// Gets ConclusionRepository
        /// </summary>
        public IGenericRepository<Conclusion> ConclusionRepository { get; }

        /// <summary>
        /// Get StatusClientRepository
        /// </summary>
        public IGenericRepository<StatusFirstCall> StatusClientRepository { get; }

        /// <summary>
        /// Get StatusClientRepository
        /// </summary>
        public IGenericRepository<SubCause> SubCauseRepository { get; }

        /// <summary>
        /// Get StatusClientRepository
        /// </summary>
        public IGenericRepository<Localization> LocalizationRepository { get; }

        /// <summary>
        /// Get ClientResultRepository
        /// </summary>
        public IGenericRepository<ClientResult> ClientResultRepository { get; }

        /// <summary>
        /// Get MutualisationPointRepository
        /// </summary>
        public IGenericRepository<MutualisationPoint> MutualisationPointRepository { get; }

        /// <summary>
        /// Get NotificationRepository
        /// </summary>
        public IGenericRepository<AlarmNotification> NotificationRepository { get; }


        /// <summary>
        /// Get EquipementRepository
        /// </summary>
        public IGenericRepository<Equipement> EquipementRepository { get; }

        #endregion

        #region Save & Dispose

        public Task SaveAsync();

        public void UnitOfWorkDispose();

        #endregion
    }
}
