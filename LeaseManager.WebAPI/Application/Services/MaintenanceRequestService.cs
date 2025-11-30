using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.WebAPI.Application.Common.Interfaces;
using static LeaseManager.WebAPI.Application.DTOs.MaintenanceRequestDTOs;

namespace LeaseManager.WebAPI.Application.Services
{
    public class MaintenanceRequestService : IMaintenanceRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MaintenanceRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MaintenanceRequestReadDto>> GetAllAsync()
        {
            var requests = await _unitOfWork.MaintenanceRequestRepository.GetAllAsync();
            return requests.Select(r => new MaintenanceRequestReadDto
            {
                Id = r.Id,
                Description = r.Description,
                RequestDate = r.RequestDate,
                Status = r.Status,
                PropertyId = r.PropertyId,
                PropertyAddress = r.Property?.Address,
                TenantId = r.TenantId,
                TenantName = r.Tenant?.FullName
            });
        }

        public async Task<MaintenanceRequestReadDto?> GetByIdAsync(int id)
        {
            var request = await _unitOfWork.MaintenanceRequestRepository.GetRequestWithDetailsAsync(id);
            if (request == null) return null;

            return new MaintenanceRequestReadDto
            {
                Id = request.Id,
                Description = request.Description,
                RequestDate = request.RequestDate,
                Status = request.Status,
                PropertyId = request.PropertyId,
                PropertyAddress = request.Property?.Address,
                TenantId = request.TenantId,
                TenantName = request.Tenant?.FullName
            };
        }

        public async Task<MaintenanceRequestReadDto> CreateAsync(MaintenanceRequestCreateDto dto)
        {
            var request = new MaintenanceRequest
            {
                Description = dto.Description,
                RequestDate = DateTime.UtcNow,
                Status = Core.Domain.Enums.MaintenanceStatus.Pending,
                PropertyId = dto.PropertyId,
                TenantId = dto.TenantId
            };

            await _unitOfWork.MaintenanceRequestRepository.AddAsync(request);
            await _unitOfWork.SaveChangesAsync();

            var createdRequest = await _unitOfWork.MaintenanceRequestRepository.GetRequestWithDetailsAsync(request.Id);

            return new MaintenanceRequestReadDto
            {
                Id = createdRequest.Id,
                Description = createdRequest.Description,
                RequestDate = createdRequest.RequestDate,
                Status = createdRequest.Status,
                PropertyId = createdRequest.PropertyId,
                PropertyAddress = createdRequest.Property?.Address,
                TenantId = createdRequest.TenantId,
                TenantName = createdRequest.Tenant?.FullName
            };
        }

        public async Task<bool> UpdateAsync(int id, MaintenanceRequestUpdateDto dto)
        {
            var request = await _unitOfWork.MaintenanceRequestRepository.GetByIdAsync(id);
            if (request == null) return false;

            if (!string.IsNullOrWhiteSpace(dto.Description))
                request.Description = dto.Description;
            if (dto.Status.HasValue)
                request.Status = dto.Status.Value;

            _unitOfWork.MaintenanceRequestRepository.Update(request);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var request = await _unitOfWork.MaintenanceRequestRepository.GetByIdAsync(id);
            if (request == null) return false;

            _unitOfWork.MaintenanceRequestRepository.Delete(request);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}