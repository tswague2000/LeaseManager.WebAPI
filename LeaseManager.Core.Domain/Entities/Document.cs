using LeaseManager.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

public class Document
{
    public int Id { get; set; }
    public int LeaseId { get; set; }

    [Required] 
    public required string FileName { get; set; }

    [Required]
    public required string FilePath { get; set; }

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    [Required] 
    public required Lease Lease { get; set; }
}
