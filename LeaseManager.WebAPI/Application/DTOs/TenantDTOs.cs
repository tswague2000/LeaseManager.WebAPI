namespace LeaseManager.WebAPI.Application.DTOs
{
    public class TenantDTOs
    {
        public class TenantCreateDto
        {
            public string FullName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
        }

        public class TenantUpdateDto
        {
            public string? FullName { get; set; }
            public string? Email { get; set; }
            public string? PhoneNumber { get; set; }
        }

        public class TenantReadDto
        {
            public int Id { get; set; }
            public string FullName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;

            public List<LeaseSummaryDto>? Leases { get; set; }
        }

        public class LeaseSummaryDto
        {
            public int Id { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }
    }
}
