using LeaseManager.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseManager.Core.Domain.Entities
{
    /// <summary>
    /// Représente un bien immobilier disponible sur la plateforme.
    /// Une propriété appartient à un propriétaire et peut avoir plusieurs images et baux.
    /// </summary>
    public class Property
    {
        #region Propriétés principales
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string? PostalCode { get; set; }
        public decimal Price { get; set; }
        public PropertyType Type { get; set; }
        public PropertyStatus Status { get; set; }
        public int Rooms { get; set; }
        public int Bathroom { get; set; }
        public double? Surface { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        #endregion

        #region Relations
        public int OwnerId { get; set; }
        public User? Owner { get; set; }
        public ICollection<PropertyImage>? Images { get; set; }
        public ICollection<Lease>? Leases { get; set; }
        #endregion
    }
}
