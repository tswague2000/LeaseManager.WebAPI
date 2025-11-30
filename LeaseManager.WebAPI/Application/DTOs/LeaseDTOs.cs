namespace LeaseManager.WebAPI.Application.DTOs
{
    public class LeaseDTOs
    {
        public class LeaseReadDto
        {
            public int Id { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public decimal MonthlyRent { get; set; }
            public int TenantId { get; set; }
            public int PropertyId { get; set; }
            public string? TenantName { get; set; }
            public string? PropertyAddress { get; set; }
        }

        public class LeaseCreateDto
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public decimal MonthlyRent { get; set; }
            public int TenantId { get; set; }
            public int PropertyId { get; set; }
        }

        public class LeaseUpdateDto
        {
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public decimal? MonthlyRent { get; set; }
        }
    }
}