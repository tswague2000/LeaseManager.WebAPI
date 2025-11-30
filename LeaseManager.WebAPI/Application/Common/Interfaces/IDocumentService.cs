using static LeaseManager.WebAPI.Application.DTOs.DocumentDTOs;

namespace LeaseManager.WebAPI.Application.Common.Interfaces
{
    public interface IDocumentService
    {
        Task<IEnumerable<DocumentReadDto>> GetAllAsync();
        Task<DocumentReadDto?> GetByIdAsync(int id);
        Task<DocumentReadDto> CreateAsync(DocumentCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}