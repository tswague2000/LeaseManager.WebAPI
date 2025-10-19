using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.FrameWork.Interface;

namespace LeaseManager.Infrastucture.Interfaces
{
    public interface ITenantRepository : IGenericRepository<Tenant>
    {
        Task<Tenant?> GetTenantWithLeasesAsync(int tenantId);
        Task<bool> TenantExistsByEmailAsync(string email);
    }
}
