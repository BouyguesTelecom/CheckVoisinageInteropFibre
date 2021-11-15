using CVI.Domain.ModelBase;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVI.Domain.Model
{
    /// <summary>
    /// The User entity class
    /// </summary>
    [Table("REF_USERS")]
    public class User: BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Login { get; set; }

        public string Profil { get; set; }

        /// <summary>
        /// Gets or sets Interventions
        /// </summary>
        public IEnumerable<Intervention.Intervention> Interventions { get; set; }
    }
}
