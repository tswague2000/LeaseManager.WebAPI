using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.Core.Infrastuctures.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaseManager.WebAPI.Application.Repository
{
    public class LeaseRepository : GenericRepository<Lease>, ILeaseRepository
    {
        public LeaseRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Lease>> GetLeasesByPropertyIdAsync(int propertyId)
        {
            return await _dbSet
                .Where(l => l.PropertyId == propertyId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Lease>> GetLeasesByTenantIdAsync(int tenantId)
        {
            return await _dbSet
                .Where(l => l.TenantId == tenantId)
                .ToListAsync();
        }

        public async Task<Lease?> GetLeaseWithDetailsAsync(int leaseId)
        {
            return await _dbSet
                .Include(l => l.Property)
                .Include(l => l.Tenant)
                .Include(l => l.Documents)
                .Include(l => l.Payments)
                .FirstOrDefaultAsync(l => l.Id == leaseId);
        }
    }
}
