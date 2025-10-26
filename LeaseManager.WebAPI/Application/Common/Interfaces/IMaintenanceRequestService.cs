using static LeaseManager.WebAPI.Application.DTOs.MaintenanceRequestDTOs;

namespace LeaseManager.WebAPI.Application.Common.Interfaces
{
    public interface IMaintenanceRequestService
    {
        Task<IEnumerable<MaintenanceRequestReadDto>> GetAllAsync();
        Task<MaintenanceRequestReadDto?> GetByIdAsync(int id);
        Task<MaintenanceRequestReadDto> CreateAsync(MaintenanceRequestCreateDto dto);
        Task<bool> UpdateAsync(int id, MaintenanceRequestUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}