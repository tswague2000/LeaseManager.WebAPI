using LeaseManager.Core.Domain.Entities;

namespace LeaseManager.Core.Domain.Interfaces
{
    public interface ITenantRepository : IGenericRepository<Tenant>
    {
 Task<Tenant?> GetTenantWithLeasesAsync(int tenantId);
        Task<bool> TenantExistsByEmailAsync(string email);
    }
}