using static LeaseManager.WebAPI.Application.DTOs.OwnerDTOs;

namespace LeaseManager.WebAPI.Application.Common.Interfaces
{
    public interface IOwnerService
    {
        Task<IEnumerable<OwnerReadDto>> GetAllAsync();
        Task<OwnerReadDto?> GetByIdAsync(int id);
        Task<OwnerReadDto> CreateAsync(OwnerCreateDto dto);
        Task<bool> UpdateAsync(int id, OwnerUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
