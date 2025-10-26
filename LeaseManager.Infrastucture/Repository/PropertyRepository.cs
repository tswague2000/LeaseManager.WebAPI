using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.Core.Infrastuctures.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaseManager.WebAPI.Application.Repository
{
    public class PropertyRepository : GenericRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Property?> GetPropertyWithDetailsAsync(int propertyId)
        {
            return await _dbSet
                .Include(p => p.Owner)
                .Include(p => p.Leases)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == propertyId);
        }

        public async Task<IEnumerable<Property>> GetPropertiesByOwnerIdAsync(int ownerId)
        {
            return await _dbSet
                .Where(p => p.OwnerId == ownerId)
                .ToListAsync();
        }
    }
}
