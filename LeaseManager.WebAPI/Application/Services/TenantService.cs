using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.WebAPI.Application.Common.Interfaces;
using static LeaseManager.WebAPI.Application.DTOs.TenantDTOs;

namespace LeaseManager.WebAPI.Application.Services
{
    public class TenantService : ITenantService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TenantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TenantReadDto>> GetAllAsync()
        {
            var tenants = await _unitOfWork.TenantRepository.GetAllAsync();

            return tenants.Select(t => new TenantReadDto
            {
                Id = t.Id,
                FullName = t.FullName,
                Email = t.Email,
                PhoneNumber = t.PhoneNumber
            });
        }

        public async Task<TenantReadDto?> GetByIdAsync(int id)
        {
            var tenant = await _unitOfWork.TenantRepository.GetTenantWithLeasesAsync(id);
            if (tenant == null) return null;

            return new TenantReadDto
            {
                Id = tenant.Id,
                FullName = tenant.FullName,
                Email = tenant.Email,
                PhoneNumber = tenant.PhoneNumber,
                Leases = tenant.Leases?.Select(l => new LeaseSummaryDto
                {
                    Id = l.Id,
                    StartDate = l.StartDate,
                    EndDate = l.EndDate
                }).ToList()
            };
        }

        public async Task<TenantReadDto> CreateAsync(TenantCreateDto dto)
        {
            var exists = await _unitOfWork.TenantRepository.TenantExistsByEmailAsync(dto.Email);
            if (exists)
                throw new InvalidOperationException("A tenant with this email already exists.");

            var tenant = new Tenant
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };

            await _unitOfWork.TenantRepository.AddAsync(tenant);
            await _unitOfWork.SaveChangesAsync();

            return new TenantReadDto
            {
                Id = tenant.Id,
                FullName = tenant.FullName,
                Email = tenant.Email,
                PhoneNumber = tenant.PhoneNumber
            };
        }

        public async Task<bool> UpdateAsync(int id, TenantUpdateDto dto)
        {
            var tenant = await _unitOfWork.TenantRepository.GetByIdAsync(id);
            if (tenant == null) return false;

            if (!string.IsNullOrWhiteSpace(dto.FullName))
                tenant.FullName = dto.FullName;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                tenant.Email = dto.Email;

            if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
                tenant.PhoneNumber = dto.PhoneNumber;

            _unitOfWork.TenantRepository.Update(tenant);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tenant = await _unitOfWork.TenantRepository.GetByIdAsync(id);
            if (tenant == null) return false;

            _unitOfWork.TenantRepository.Delete(tenant);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
