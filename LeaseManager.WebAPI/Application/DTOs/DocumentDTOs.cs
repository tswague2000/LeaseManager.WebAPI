namespace LeaseManager.WebAPI.Application.DTOs
{
    public class DocumentDTOs
    {
        public class DocumentReadDto
        {
            public int Id { get; set; }
            public string FileName { get; set; } = string.Empty;
            public string FilePath { get; set; } = string.Empty;
            public DateTime UploadedAt { get; set; }
            public int LeaseId { get; set; }
        }

        public class DocumentCreateDto
        {
            public string FileName { get; set; } = string.Empty;
            public string FilePath { get; set; } = string.Empty;
            public int LeaseId { get; set; }
        }
    }
}