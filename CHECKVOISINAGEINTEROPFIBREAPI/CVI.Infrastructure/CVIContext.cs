using CVI.Domain.Model;
using CVI.Domain.Model.Alarms;
using CVI.Domain.Model.Intervention;
using CVI.Domain.Model.Photo;
using CVI.Domain.Model.Referential;
using Microsoft.EntityFrameworkCore;

namespace CVI.Infrastructure
{
    /// <summary>
    /// The CVIContext class
    /// </summary>
    public class CviContext: DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public CviContext(DbContextOptions options) :
            base(options)
        {
        }

        /// <summary>
        /// Gets or sets Interventions
        /// </summary>
        public virtual DbSet<Intervention> Interventions { get; set; }

        /// <summary>
        /// Gets or sets InterventionTypes
        /// </summary>
        public virtual DbSet<InterventionType> InterventionTypes { get; set; }

        /// <summary>
        /// Gets or sets Photos
        /// </summary>
        public virtual DbSet<Photo> Photos { get; set; }

        /// <summary>
        /// Gets or sets PhotoResults
        /// </summary>
        public virtual DbSet<PhotoResult> PhotoResults { get; set; }

        /// <summary>
        /// Gets or sets Alarms
        /// </summary>
        public virtual DbSet<Alarm> Alarms { get; set; }


        /// <summary>
        /// Gets or sets Operators
        /// </summary>
        public virtual DbSet<Operator> Operators { get; set; }

        /// <summary>
        /// Gets or sets ClientPhotoResults
        /// </summary>
        public virtual DbSet<ClientResult> ClientPhotoResults { get; set; }

        /// <summary>
        /// Gets or sets Conclusions
        /// </summary>
        public virtual DbSet<Conclusion> Conclusions { get; set; }

        /// <summary>
        /// Gets or sets Localizations
        /// </summary>
        public virtual DbSet<Localization> Localizations { get; set; }

        /// <summary>
        /// Gets or sets StatusFirstCalls
        /// </summary>
        public virtual DbSet<StatusFirstCall> StatusFirstCalls { get; set; }

        /// <summary>
        /// Gets or sets SubCauses
        /// </summary>
        public virtual DbSet<SubCause> SubCauses { get; set; }

        /// <summary>
        /// Gets or sets Users
        /// </summary>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Get or sets notifications
        /// </summary>
        public virtual DbSet<AlarmNotification> Notifications { get; set; }

        /// <summary>
        /// Get or sets Euipements
        /// </summary>
        public virtual DbSet<Equipement> Euipements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Localization>()
                .HasMany(s => s.SubCauses)
                .WithMany(c => c.Localizations).UsingEntity(cs => cs.ToTable("LOC_SUB_CAUSES"));

            modelBuilder.DataBenchAlarmsNotifications();
        }
    }
}
