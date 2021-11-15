using CVI.Domain.IRepository;
using CVI.Domain.Model;
using CVI.Domain.Model.Intervention;
using CVI.Domain.Model.Photo;
using CVI.Infrastructure.Repository;
using System;
using System.Threading.Tasks;
using CVI.Domain.Common;
using CVI.Domain.Model.Alarms;
using CVI.Domain.Model.Referential;

namespace CVI.Infrastructure
{
    /// <summary>
    /// The UnitOfWork class.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region private properties

        /// <summary>
        /// Context
        /// </summary>
        private readonly CviContext _context;

        /// <summary>
        /// Disposed attribute
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Gets _interventionRepository
        /// </summary>
        private IGenericRepository<Intervention> _interventionRepository;

        /// <summary>
        /// Gets _interventionTypeRepository
        /// </summary>
        private IGenericRepository<InterventionType> _interventionTypeRepository;

        /// <summary>
        /// Get _photoRepository
        /// </summary>
        private IGenericRepository<Photo> _photoRepository;

        /// <summary>
        /// Gets _photoResultRepository
        /// </summary>
        private IGenericRepository<PhotoResult> _photoResultRepository;

        /// <summary>
        /// Gets _alarmRepository
        /// </summary>
        private IGenericRepository<Alarm> _alarmRepository;

        /// <summary>
        /// Gets _operatorRepository
        /// </summary>
        private IGenericRepository<Operator> _operatorRepository;

        /// <summary>
        /// Gets _rejectCauseRepository
        /// </summary>
        private IGenericRepository<ClientResult> _rejectCauseRepository;

        /// <summary>
        /// Gets _userRepository
        /// </summary>
        private IGenericRepository<User> _userRepository;

        /// <summary>
        /// Gets _conclusionRepository
        /// </summary>
        private IGenericRepository<Conclusion> _conclusionRepository;

        /// <summary>
        /// Get _statusClientRepository
        /// </summary>
        private IGenericRepository<StatusFirstCall> _statusClientRepository;

        /// <summary>
        /// Get _subCauseRepository
        /// </summary>
        private IGenericRepository<SubCause> _subCauseRepository;

        /// <summary>
        /// Get _localizationRepository
        /// </summary>
        private IGenericRepository<Localization> _localizationRepository;

        /// <summary>
        /// Get _clientResultRepository
        /// </summary>
        private IGenericRepository<ClientResult> _clientResultRepository;

        /// <summary>
        /// Get _mutualisationPointRepository
        /// </summary>
        private IGenericRepository<MutualisationPoint> _mutualisationPointRepository;

        /// <summary>
        /// The _notificationRepository
        /// </summary>
        private IGenericRepository<AlarmNotification> _notificationRepository;

        /// <summary>
        /// The _equipementRepository
        /// </summary>
        private IGenericRepository<Equipement> _equipementRepository;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(CviContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region protected methods

        /// <summary>
        /// Dispose context method
        /// </summary>
        /// <param name="disposing">Disposing value</param>
        protected virtual void UnitOfWorkDispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        #endregion

        #region Publics methods

        /// <summary>
        /// The InterventionRepository
        /// </summary>
        public IGenericRepository<Intervention> InterventionRepository => _interventionRepository ??= new GenericRepository<Intervention>(_context);

        /// <summary>
        /// The InterventionTypeRepository
        /// </summary>
        public IGenericRepository<InterventionType> InterventionTypeRepository => _interventionTypeRepository ??= new GenericRepository<InterventionType>(_context);

        /// <summary>
        /// The PhotoRepository
        /// </summary>
        public IGenericRepository<Photo> PhotoRepository => _photoRepository ??= new GenericRepository<Photo>(_context);

        /// <summary>
        /// The PhotoRepository
        /// </summary>
        public IGenericRepository<PhotoResult> PhotoResultRepository => _photoResultRepository ??= new GenericRepository<PhotoResult>(_context);

        /// <summary>
        /// The AlarmRepository
        /// </summary>
        public IGenericRepository<Alarm> AlarmRepository => _alarmRepository ??= new GenericRepository<Alarm>(_context);

        /// <summary>
        /// The OperatorRepository
        /// </summary>
        public IGenericRepository<Operator> OperatorRepository => _operatorRepository ??= new GenericRepository<Operator>(_context);

        /// <summary>
        /// The UserRepository
        /// </summary>
        public IGenericRepository<User> UserRepository => _userRepository ??= new GenericRepository<User>(_context);

        /// <summary>
        /// The UserRepository
        /// </summary>
        public IGenericRepository<ClientResult> RejectCauseRepository => _rejectCauseRepository ??= new GenericRepository<ClientResult>(_context);

        /// <summary>
        /// The ConclusionRepository
        /// </summary>
        public IGenericRepository<Conclusion> ConclusionRepository => _conclusionRepository ??= new GenericRepository<Conclusion>(_context);

        /// <summary>
        /// The StatusClientRepository
        /// </summary>
        public IGenericRepository<StatusFirstCall> StatusClientRepository => _statusClientRepository ??= new GenericRepository<StatusFirstCall>(_context);

        /// <summary>
        /// The SubCauseRepository
        /// </summary>
        public IGenericRepository<SubCause> SubCauseRepository => _subCauseRepository ??= new GenericRepository<SubCause>(_context);

        /// <summary>
        /// The LocalizationRepository
        /// </summary>
        public IGenericRepository<Localization> LocalizationRepository => _localizationRepository ??= new GenericRepository<Localization>(_context);

        /// <summary>
        /// The ClientResultRepository
        /// </summary>
        public IGenericRepository<ClientResult> ClientResultRepository => _clientResultRepository ??= new GenericRepository<ClientResult>(_context);

        /// <summary>
        /// The MutualisationPointRepository
        /// </summary>
        public IGenericRepository<MutualisationPoint> MutualisationPointRepository => _mutualisationPointRepository ??= new GenericRepository<MutualisationPoint>(_context);

        /// <summary>
        /// The MutualisationPointRepository
        /// </summary>
        public IGenericRepository<AlarmNotification> NotificationRepository => _notificationRepository ??= new GenericRepository<AlarmNotification>(_context);

        /// <summary>
        /// The MutualisationPointRepository
        /// </summary>
        public IGenericRepository<Equipement> EquipementRepository => _equipementRepository ??= new GenericRepository<Equipement>(_context);

        /// <summary>
        /// Save change method
        /// </summary>
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Dispose Method
        /// </summary>
        public void UnitOfWorkDispose()
        {
            UnitOfWorkDispose(true);
        }

        #endregion
    }
}
