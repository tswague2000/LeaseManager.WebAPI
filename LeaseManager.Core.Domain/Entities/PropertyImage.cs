namespace LeaseManager.Core.Domain.Entities
{
    /// <summary>
    /// Représente une image associée à un bien immobilier.
    /// </summary>
    public class PropertyImage
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
