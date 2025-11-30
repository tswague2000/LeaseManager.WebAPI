using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.WebAPI.Application.Common.Interfaces;
using static LeaseManager.WebAPI.Application.DTOs.PropertyImageDTOs;

namespace LeaseManager.WebAPI.Application.Services
{
    public class PropertyImageService : IPropertyImageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PropertyImageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PropertyImageReadDto>> GetAllAsync()
        {
            var images = await _unitOfWork.PropertyImageRepository.GetAllAsync();
            return images.Select(i => new PropertyImageReadDto
            {
                Id = i.Id,
                ImageUrl = i.ImageUrl,
                Description = i.Description,
                PropertyId = i.PropertyId,
                PropertyAddress = i.Property?.Address
            });
        }

        public async Task<PropertyImageReadDto?> GetByIdAsync(int id)
        {
            var image = await _unitOfWork.PropertyImageRepository.GetByIdAsync(id);
            if (image == null) return null;

            return new PropertyImageReadDto
            {
                Id = image.Id,
                ImageUrl = image.ImageUrl,
                Description = image.Description,
                PropertyId = image.PropertyId,
                PropertyAddress = image.Property?.Address
            };
        }

        public async Task<PropertyImageReadDto> CreateAsync(PropertyImageCreateDto dto)
        {
            var property = await _unitOfWork.PropertyRepository.GetByIdAsync(dto.PropertyId);
            if (property == null)
                throw new InvalidOperationException($"Property with ID {dto.PropertyId} not found");

            var image = new PropertyImage
            {
                ImageUrl = dto.ImageUrl,
                Description = dto.Description,
                PropertyId = dto.PropertyId,
                Property = property
            };

            await _unitOfWork.PropertyImageRepository.AddAsync(image);
            await _unitOfWork.SaveChangesAsync();

            return new PropertyImageReadDto
            {
                Id = image.Id,
                ImageUrl = image.ImageUrl,
                Description = image.Description,
                PropertyId = image.PropertyId,
                PropertyAddress = property.Address
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var image = await _unitOfWork.PropertyImageRepository.GetByIdAsync(id);
            if (image == null) return false;

            _unitOfWork.PropertyImageRepository.Delete(image);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}