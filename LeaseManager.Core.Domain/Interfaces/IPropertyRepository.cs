using LeaseManager.Core.Domain.Entities;

namespace LeaseManager.Core.Domain.Interfaces
{
    public interface IPropertyRepository : IGenericRepository<Property>
    {
        Task<Property?> GetPropertyWithDetailsAsync(int propertyId);
    Task<IEnumerable<Property>> GetPropertiesByOwnerIdAsync(int ownerId);
    }
}