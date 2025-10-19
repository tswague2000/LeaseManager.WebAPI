using LeaseManager.Core.Domain.Enums;

namespace LeaseManager.Core.Domain.Entities
{
    /// <summary>
    /// Représente un contrat de location entre un locataire et un propriétaire.
    /// </summary>
    public class Lease
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MonthlyRent { get; set; }
        public LeaseStatus Status { get; set; } = LeaseStatus.Active;

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Document> Documents { get; set; } = new List<Document>();

    }
}
