using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Infrastuctures.Data;
using LeaseManager.Infrastucture.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LeaseManager.WebAPI.Application.Repository
{
    public class OwnerRepository(AppDbContext context) : GenericRepository<Owner>(context), IOwnerRepository
    {

        public async Task<Owner?> GetOwnerWithPropertiesAsync(int ownerId)
        {
            return await _dbSet
                .Include(o => o.Properties)
                .FirstOrDefaultAsync(o => o.Id == ownerId);
        }

        public async Task<bool> OwnerExistsByEmailAsync(string email)
        {
            return await _dbSet.AnyAsync(o => o.Email == email);
        }
    }
}
