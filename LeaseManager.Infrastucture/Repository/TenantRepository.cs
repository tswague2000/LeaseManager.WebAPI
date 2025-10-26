using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.Core.Infrastuctures.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaseManager.WebAPI.Application.Repository
{
    public class TenantRepository : GenericRepository<Tenant>, ITenantRepository
    {
        public TenantRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Tenant?> GetTenantWithLeasesAsync(int tenantId)
        {
            return await _dbSet
                .Include(t => t.Leases)
                .FirstOrDefaultAsync(t => t.Id == tenantId);
        }

        public async Task<bool> TenantExistsByEmailAsync(string email)
        {
            return await _dbSet.AnyAsync(t => t.Email == email);
        }
    }
}
