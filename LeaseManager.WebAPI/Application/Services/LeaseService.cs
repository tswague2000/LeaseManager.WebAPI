using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.WebAPI.Application.Common.Interfaces;
using static LeaseManager.WebAPI.Application.DTOs.LeaseDTOs;

namespace LeaseManager.WebAPI.Application.Services
{
    public class LeaseService : ILeaseService
    {
    private readonly IUnitOfWork _unitOfWork;

        public LeaseService(IUnitOfWork unitOfWork)
        {
    _unitOfWork = unitOfWork;
}

 public async Task<IEnumerable<LeaseReadDto>> GetAllAsync()
  {
   var leases = await _unitOfWork.LeaseRepository.GetAllAsync();
    return leases.Select(l => new LeaseReadDto
   {
  Id = l.Id,
         StartDate = l.StartDate,
     EndDate = l.EndDate,
      MonthlyRent = l.MonthlyRent,
TenantId = l.TenantId,
   PropertyId = l.PropertyId
       });
 }

        public async Task<LeaseReadDto?> GetByIdAsync(int id)
 {
        var lease = await _unitOfWork.LeaseRepository.GetByIdAsync(id);
  if (lease == null) return null;

   return new LeaseReadDto
            {
  Id = lease.Id,
     StartDate = lease.StartDate,
 EndDate = lease.EndDate,
    MonthlyRent = lease.MonthlyRent,
        TenantId = lease.TenantId,
     PropertyId = lease.PropertyId
   };
}

    public async Task<LeaseReadDto> CreateAsync(LeaseCreateDto dto)
        {
  var property = await _unitOfWork.PropertyRepository.GetByIdAsync(dto.PropertyId);
      if (property == null)
       throw new InvalidOperationException($"Property with ID {dto.PropertyId} not found");

   var tenant = await _unitOfWork.TenantRepository.GetByIdAsync(dto.TenantId);
  if (tenant == null)
    throw new InvalidOperationException($"Tenant with ID {dto.TenantId} not found");

         var lease = new Lease
     {
     StartDate = dto.StartDate,
 EndDate = dto.EndDate,
       MonthlyRent = dto.MonthlyRent,
 TenantId = dto.TenantId,
   PropertyId = dto.PropertyId,
       Property = property,
         Tenant = tenant
};

   await _unitOfWork.LeaseRepository.AddAsync(lease);
     await _unitOfWork.SaveChangesAsync();

 return new LeaseReadDto
      {
      Id = lease.Id,
      StartDate = lease.StartDate,
EndDate = lease.EndDate,
      MonthlyRent = lease.MonthlyRent,
        TenantId = lease.TenantId,
 PropertyId = lease.PropertyId,
  TenantName = tenant.FullName,
 PropertyAddress = property.Address
      };
    }

        public async Task<bool> UpdateAsync(int id, LeaseUpdateDto dto)
{
 var lease = await _unitOfWork.LeaseRepository.GetByIdAsync(id);
   if (lease == null) return false;

          if (dto.StartDate.HasValue)
  lease.StartDate = dto.StartDate.Value;
      if (dto.EndDate.HasValue)
  lease.EndDate = dto.EndDate.Value;
    if (dto.MonthlyRent.HasValue)
  lease.MonthlyRent = dto.MonthlyRent.Value;

     _unitOfWork.LeaseRepository.Update(lease);
await _unitOfWork.SaveChangesAsync();

        return true;
      }

    public async Task<bool> DeleteAsync(int id)
        {
     var lease = await _unitOfWork.LeaseRepository.GetByIdAsync(id);
       if (lease == null) return false;

         _unitOfWork.LeaseRepository.Delete(lease);
     await _unitOfWork.SaveChangesAsync();

    return true;
        }
    }
}