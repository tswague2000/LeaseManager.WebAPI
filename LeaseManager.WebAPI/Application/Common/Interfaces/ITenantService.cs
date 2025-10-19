using static LeaseManager.WebAPI.Application.DTOs.TenantDTOs;

namespace LeaseManager.WebAPI.Application.Common.Interfaces
{
    public interface ITenantService
    {
        Task<IEnumerable<TenantReadDto>> GetAllAsync();
        Task<TenantReadDto?> GetByIdAsync(int id);
        Task<TenantReadDto> CreateAsync(TenantCreateDto dto);
        Task<bool> UpdateAsync(int id, TenantUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
