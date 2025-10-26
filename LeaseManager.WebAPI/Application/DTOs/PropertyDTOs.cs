namespace LeaseManager.WebAPI.Application.DTOs
{
    public class PropertyDTOs
    {
        public class PropertyReadDto
        {
            public int Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
            public string City { get; set; } = string.Empty;
            public string Province { get; set; } = string.Empty;
            public string PostalCode { get; set; } = string.Empty;
            public decimal RentPrice { get; set; }
            public int Bedrooms { get; set; }
            public int Bathrooms { get; set; }
            public bool IsAvailable { get; set; }
            public int OwnerId { get; set; }
            public string? OwnerName { get; set; }
        }

        public class PropertyCreateDto
        {
            public string Title { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
            public string City { get; set; } = string.Empty;
            public string Province { get; set; } = string.Empty;
            public string PostalCode { get; set; } = string.Empty;
            public decimal RentPrice { get; set; }
            public int Bedrooms { get; set; }
            public int Bathrooms { get; set; }
            public bool IsAvailable { get; set; } = true;
            public int OwnerId { get; set; }
        }

        public class PropertyUpdateDto
        {
            public string? Title { get; set; }
            public string? Address { get; set; }
            public string? City { get; set; }
            public string? Province { get; set; }
            public string? PostalCode { get; set; }
            public decimal? RentPrice { get; set; }
            public int? Bedrooms { get; set; }
            public int? Bathrooms { get; set; }
            public bool? IsAvailable { get; set; }
        }
    }
}