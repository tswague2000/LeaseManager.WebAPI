using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseManager.Core.Domain.Enums
{
    /// <summary>
    /// Rôle d’un utilisateur dans le système.
    /// </summary>
    public enum UserRole
    {
        Tenant = 1,     // Locataire
        Landlord = 2,   // Propriétaire
        Admin = 3       // Administrateur
    }
}
