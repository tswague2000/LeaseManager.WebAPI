using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.WebAPI.Application.Common.Interfaces;
using static LeaseManager.WebAPI.Application.DTOs.PropertyDTOs;

namespace LeaseManager.WebAPI.Application.Services
{
    public class PropertyService : IPropertyService
 {
        private readonly IUnitOfWork _unitOfWork;

        public PropertyService(IUnitOfWork unitOfWork)
        {
       _unitOfWork = unitOfWork;
  }

        public async Task<IEnumerable<PropertyReadDto>> GetAllAsync()
        {
            var properties = await _unitOfWork.PropertyRepository.GetAllAsync();
     return properties.Select(p => new PropertyReadDto
   {
         Id = p.Id,
Title = p.Title,
 Address = p.Address,
             City = p.City,
         Province = p.Province,
         PostalCode = p.PostalCode,
     RentPrice = p.RentPrice,
       Bedrooms = p.Bedrooms,
   Bathrooms = p.Bathrooms,
              IsAvailable = p.IsAvailable,
     OwnerId = p.OwnerId,
   OwnerName = p.Owner?.FullName
      });
}

      public async Task<PropertyReadDto?> GetByIdAsync(int id)
    {
            var property = await _unitOfWork.PropertyRepository.GetByIdAsync(id);
         if (property == null) return null;

            return new PropertyReadDto
            {
                Id = property.Id,
      Title = property.Title,
    Address = property.Address,
        City = property.City,
   Province = property.Province,
PostalCode = property.PostalCode,
        RentPrice = property.RentPrice,
         Bedrooms = property.Bedrooms,
     Bathrooms = property.Bathrooms,
     IsAvailable = property.IsAvailable,
       OwnerId = property.OwnerId,
                OwnerName = property.Owner?.FullName
            };
        }

      public async Task<PropertyReadDto> CreateAsync(PropertyCreateDto dto)
        {
            var owner = await _unitOfWork.OwnerRepository.GetByIdAsync(dto.OwnerId);
     if (owner == null)
         throw new InvalidOperationException($"Owner with ID {dto.OwnerId} not found");

            var property = new Property
            {
       Title = dto.Title,
     Address = dto.Address,
       City = dto.City,
      Province = dto.Province,
    PostalCode = dto.PostalCode,
  RentPrice = dto.RentPrice,
        Bedrooms = dto.Bedrooms,
                Bathrooms = dto.Bathrooms,
     IsAvailable = dto.IsAvailable,
           OwnerId = dto.OwnerId,
                Owner = owner,
         Images = new List<PropertyImage>(),
Leases = new List<Lease>()
     };

            await _unitOfWork.PropertyRepository.AddAsync(property);
   await _unitOfWork.SaveChangesAsync();

      return new PropertyReadDto
 {
     Id = property.Id,
  Title = property.Title,
         Address = property.Address,
     City = property.City,
       Province = property.Province,
       PostalCode = property.PostalCode,
     RentPrice = property.RentPrice,
  Bedrooms = property.Bedrooms,
    Bathrooms = property.Bathrooms,
       IsAvailable = property.IsAvailable,
   OwnerId = property.OwnerId,
     OwnerName = owner.FullName
    };
   }

        public async Task<bool> UpdateAsync(int id, PropertyUpdateDto dto)
        {
            var property = await _unitOfWork.PropertyRepository.GetByIdAsync(id);
         if (property == null) return false;

      if (!string.IsNullOrWhiteSpace(dto.Title))
                property.Title = dto.Title;
            if (!string.IsNullOrWhiteSpace(dto.Address))
           property.Address = dto.Address;
    if (!string.IsNullOrWhiteSpace(dto.City))
     property.City = dto.City;
   if (!string.IsNullOrWhiteSpace(dto.Province))
     property.Province = dto.Province;
  if (!string.IsNullOrWhiteSpace(dto.PostalCode))
      property.PostalCode = dto.PostalCode;
            if (dto.RentPrice.HasValue)
                property.RentPrice = dto.RentPrice.Value;
     if (dto.Bedrooms.HasValue)
             property.Bedrooms = dto.Bedrooms.Value;
   if (dto.Bathrooms.HasValue)
        property.Bathrooms = dto.Bathrooms.Value;
   if (dto.IsAvailable.HasValue)
     property.IsAvailable = dto.IsAvailable.Value;

            _unitOfWork.PropertyRepository.Update(property);
 await _unitOfWork.SaveChangesAsync();

      return true;
      }

        public async Task<bool> DeleteAsync(int id)
        {
   var property = await _unitOfWork.PropertyRepository.GetByIdAsync(id);
   if (property == null) return false;

            _unitOfWork.PropertyRepository.Delete(property);
    await _unitOfWork.SaveChangesAsync();

            return true;
  }
    }
}