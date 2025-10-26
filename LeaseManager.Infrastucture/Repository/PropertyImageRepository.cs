using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.Core.Infrastuctures.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaseManager.WebAPI.Application.Repository
{
    public class PropertyImageRepository : GenericRepository<PropertyImage>, IPropertyImageRepository
    {
        public PropertyImageRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PropertyImage>> GetImagesByPropertyIdAsync(int propertyId)
        {
            return await _dbSet
                .Where(i => i.PropertyId == propertyId)
                .ToListAsync();
        }
    }
}
