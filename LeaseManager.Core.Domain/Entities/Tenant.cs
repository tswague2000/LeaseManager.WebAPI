namespace LeaseManager.Core.Domain.Entities
{
    public class Tenant
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Lease> Leases { get; set; }
    }

}
