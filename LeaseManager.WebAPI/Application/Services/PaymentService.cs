using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.WebAPI.Application.Common.Interfaces;
using static LeaseManager.WebAPI.Application.DTOs.PaymentDTOs;

namespace LeaseManager.WebAPI.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PaymentReadDto>> GetAllAsync()
        {
            var payments = await _unitOfWork.PaymentRepository.GetAllAsync();
            return payments.Select(p => new PaymentReadDto
            {
                Id = p.Id,
                LeaseId = p.LeaseId,
                Amount = p.Amount,
                PaymentDate = p.PaymentDate,
                Status = p.Status,
                Method = p.Method
            });
        }

        public async Task<PaymentReadDto?> GetByIdAsync(int id)
        {
            var payment = await _unitOfWork.PaymentRepository.GetPaymentWithDetailsAsync(id);
            if (payment == null) return null;

            return new PaymentReadDto
            {
                Id = payment.Id,
                LeaseId = payment.LeaseId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                Status = payment.Status,
                Method = payment.Method
            };
        }

        public async Task<PaymentReadDto> CreateAsync(PaymentCreateDto dto)
        {
            var payment = new Payment
            {
                LeaseId = dto.LeaseId,
                Amount = dto.Amount,
                PaymentDate = dto.PaymentDate,
                Status = dto.Status,
                Method = dto.Method
            };

            await _unitOfWork.PaymentRepository.AddAsync(payment);
            await _unitOfWork.SaveChangesAsync();

            return new PaymentReadDto
            {
                Id = payment.Id,
                LeaseId = payment.LeaseId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                Status = payment.Status,
                Method = payment.Method
            };
        }

        public async Task<bool> UpdateAsync(int id, PaymentUpdateDto dto)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(id);
            if (payment == null) return false;

            if (dto.Amount.HasValue)
                payment.Amount = dto.Amount.Value;
            if (dto.PaymentDate.HasValue)
                payment.PaymentDate = dto.PaymentDate.Value;
            if (dto.Status.HasValue)
                payment.Status = dto.Status.Value;
            if (dto.Method.HasValue)
                payment.Method = dto.Method.Value;

            _unitOfWork.PaymentRepository.Update(payment);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(id);
            if (payment == null) return false;

            _unitOfWork.PaymentRepository.Delete(payment);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}