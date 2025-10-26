using static LeaseManager.WebAPI.Application.DTOs.PaymentDTOs;

namespace LeaseManager.WebAPI.Application.Common.Interfaces
{
    public interface IPaymentService
    {
  Task<IEnumerable<PaymentReadDto>> GetAllAsync();
        Task<PaymentReadDto?> GetByIdAsync(int id);
    Task<PaymentReadDto> CreateAsync(PaymentCreateDto dto);
        Task<bool> UpdateAsync(int id, PaymentUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}