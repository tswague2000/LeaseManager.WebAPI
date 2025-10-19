namespace LeaseManager.WebAPI.Application.DTOs
{
    public class OwnerDTOs
    {
        public class OwnerCreateDto
        {
            public string FullName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
        }

        public class OwnerUpdateDto
        {
            public string? FullName { get; set; }
            public string? Email { get; set; }
            public string? PhoneNumber { get; set; }
        }

        public class OwnerReadDto
        {
            public int Id { get; set; }
            public string FullName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;

            // Pour les propriétés associées
            public List<PropertySummaryDto>? Properties { get; set; }
        }

        public class PropertySummaryDto
        {
            public int Id { get; set; }
            public string Address { get; set; } = string.Empty;
        }
    }
}
