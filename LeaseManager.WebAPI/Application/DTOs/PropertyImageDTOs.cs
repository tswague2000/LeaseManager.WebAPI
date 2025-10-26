namespace LeaseManager.WebAPI.Application.DTOs
{
    public class PropertyImageDTOs
    {
public class PropertyImageReadDto
        {
 public int Id { get; set; }
            public string ImageUrl { get; set; } = string.Empty;
          public string Description { get; set; } = string.Empty;
            public int PropertyId { get; set; }
            public string? PropertyAddress { get; set; }
      }

        public class PropertyImageCreateDto
        {
      public string ImageUrl { get; set; } = string.Empty;
         public string Description { get; set; } = string.Empty;
            public int PropertyId { get; set; }
        }
    }
}