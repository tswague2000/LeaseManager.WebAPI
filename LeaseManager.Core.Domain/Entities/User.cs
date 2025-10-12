using LeaseManager.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseManager.Core.Domain.Entities
{
    /// <summary>
    /// Représente un utilisateur de la plateforme.
    /// Un utilisateur peut être un locataire, un propriétaire ou un administrateur.
    /// </summary>
    public class User
    {
        #region Propriétés principales
        /// <summary>
        /// Identifiant unique de l'utilisateur.
        /// </summary>
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        #endregion

        #region Relations
        public ICollection<Property>? OwnedProperties { get; set; }
        public ICollection<Lease>? Leases { get; set; }
        #endregion
    }
}
