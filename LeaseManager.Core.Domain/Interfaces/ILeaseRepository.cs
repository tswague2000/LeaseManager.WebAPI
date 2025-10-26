using LeaseManager.Core.Domain.Entities;

namespace LeaseManager.Core.Domain.Interfaces
{
 public interface ILeaseRepository : IGenericRepository<Lease>
    {
        Task<IEnumerable<Lease>> GetLeasesByPropertyIdAsync(int propertyId);
        Task<IEnumerable<Lease>> GetLeasesByTenantIdAsync(int tenantId);
        Task<Lease?> GetLeaseWithDetailsAsync(int leaseId);
    }
}