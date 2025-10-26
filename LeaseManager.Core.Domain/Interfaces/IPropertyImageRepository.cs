using LeaseManager.Core.Domain.Entities;

namespace LeaseManager.Core.Domain.Interfaces
{
    public interface IPropertyImageRepository : IGenericRepository<PropertyImage>
  {
        Task<IEnumerable<PropertyImage>> GetImagesByPropertyIdAsync(int propertyId);
    }
}