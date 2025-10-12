using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseManager.Core.Domain.Entities
{
    /// <summary>
    /// Représente un contrat de location entre un locataire et un propriétaire.
    /// </summary>
    public class Lease
    {
        #region Propriétés principales

        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MonthlyRent { get; set; }
        #endregion

        #region Relations
        public int PropertyId { get; set; }
        public Property? Property { get; set; }

        public int TenantId { get; set; }
        public User? Tenant { get; set; }

        public ICollection<Payment>? Payments { get; set; }
        #endregion
    }
}
