using static LeaseManager.WebAPI.Application.DTOs.LeaseDTOs;

namespace LeaseManager.WebAPI.Application.Common.Interfaces
{
    public interface ILeaseService
    {
        Task<IEnumerable<LeaseReadDto>> GetAllAsync();
        Task<LeaseReadDto?> GetByIdAsync(int id);
        Task<LeaseReadDto> CreateAsync(LeaseCreateDto dto);
     Task<bool> UpdateAsync(int id, LeaseUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}