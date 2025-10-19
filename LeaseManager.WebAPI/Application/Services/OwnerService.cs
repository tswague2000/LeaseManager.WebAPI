using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.FrameWork.Interface;
using LeaseManager.WebAPI.Application.Common.Interfaces;
using static LeaseManager.WebAPI.Application.DTOs.OwnerDTOs;

namespace LeaseManager.WebAPI.Application.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OwnerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OwnerReadDto>> GetAllAsync()
        {
            var owners = await _unitOfWork.OwnerRepository.GetAllAsync();

            return owners.Select(o => new OwnerReadDto
            {
                Id = o.Id,
                FullName = o.FullName,
                Email = o.Email,
                PhoneNumber = o.PhoneNumber
            });
        }

        public async Task<OwnerReadDto?> GetByIdAsync(int id)
        {
            var owner = await _unitOfWork.OwnerRepository.GetOwnerWithPropertiesAsync(id);
            if (owner == null) return null;

            return new OwnerReadDto
            {
                Id = owner.Id,
                FullName = owner.FullName,
                Email = owner.Email,
                PhoneNumber = owner.PhoneNumber,
                Properties = owner.Properties?.Select(p => new PropertySummaryDto
                {
                    Id = p.Id,
                    Address = p.Address
                }).ToList()
            };
        }

        public async Task<OwnerReadDto> CreateAsync(OwnerCreateDto dto)
        {
            var exists = await _unitOfWork.OwnerRepository.OwnerExistsByEmailAsync(dto.Email);
            if (exists)
                throw new InvalidOperationException("An owner with this email already exists.");

            var owner = new Owner
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };

            await _unitOfWork.OwnerRepository.AddAsync(owner);
            await _unitOfWork.SaveChangesAsync();

            return new OwnerReadDto
            {
                Id = owner.Id,
                FullName = owner.FullName,
                Email = owner.Email,
                PhoneNumber = owner.PhoneNumber
            };
        }

        public async Task<bool> UpdateAsync(int id, OwnerUpdateDto dto)
        {
            var owner = await _unitOfWork.OwnerRepository.GetByIdAsync(id);
            if (owner == null) return false;

            if (!string.IsNullOrWhiteSpace(dto.FullName))
                owner.FullName = dto.FullName;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                owner.Email = dto.Email;

            if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
                owner.PhoneNumber = dto.PhoneNumber;

            _unitOfWork.OwnerRepository.Update(owner);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var owner = await _unitOfWork.OwnerRepository.GetByIdAsync(id);
            if (owner == null) return false;

            _unitOfWork.OwnerRepository.Delete(owner);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
