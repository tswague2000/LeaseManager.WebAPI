using LeaseManager.Core.Domain.Enums;

namespace LeaseManager.WebAPI.Application.DTOs
{
    public class MaintenanceRequestDTOs
    {
        public class MaintenanceRequestReadDto
        {
            public int Id { get; set; }
            public string Description { get; set; } = string.Empty;
            public DateTime RequestDate { get; set; }
            public MaintenanceStatus Status { get; set; }
            public int PropertyId { get; set; }
            public string? PropertyAddress { get; set; }
            public int TenantId { get; set; }
            public string? TenantName { get; set; }
        }

        public class MaintenanceRequestCreateDto
        {
            public string Description { get; set; } = string.Empty;
            public int PropertyId { get; set; }
            public int TenantId { get; set; }
        }

        public class MaintenanceRequestUpdateDto
        {
            public string? Description { get; set; }
            public MaintenanceStatus? Status { get; set; }
        }
    }
}