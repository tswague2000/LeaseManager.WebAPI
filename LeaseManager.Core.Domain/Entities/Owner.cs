namespace LeaseManager.Core.Domain.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Property> Properties { get; set; }
    }
}
