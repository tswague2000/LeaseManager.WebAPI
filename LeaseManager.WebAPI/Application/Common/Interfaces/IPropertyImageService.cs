using static LeaseManager.WebAPI.Application.DTOs.PropertyImageDTOs;

namespace LeaseManager.WebAPI.Application.Common.Interfaces
{
public interface IPropertyImageService
    {
        Task<IEnumerable<PropertyImageReadDto>> GetAllAsync();
        Task<PropertyImageReadDto?> GetByIdAsync(int id);
        Task<PropertyImageReadDto> CreateAsync(PropertyImageCreateDto dto);
   Task<bool> DeleteAsync(int id);
    }
}