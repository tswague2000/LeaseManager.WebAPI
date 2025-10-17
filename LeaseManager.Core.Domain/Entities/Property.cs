using LeaseManager.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Address { get; set; } = string.Empty;

        [Required]
        public required string City { get; set; } = string.Empty;

        [Required]
        public required string Province { get; set; } = string.Empty;

        [Required]
        public required string PostalCode { get; set; } = string.Empty;

        [Required]
        public decimal RentPrice { get; set; }

        [Required]
        public int Bedrooms { get; set; }

        [Required]
        public int Bathrooms { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int OwnerId { get; set; }

        [Required]
        public required Owner Owner { get; set; }

        #endregion

        #region Relations

        [Required]
        public ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();

        [Required]
        public ICollection<Lease> Leases { get; set; } = new List<Lease>();

        #endregion
    }
}