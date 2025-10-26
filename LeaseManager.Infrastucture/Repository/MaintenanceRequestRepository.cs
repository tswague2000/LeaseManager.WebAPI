using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Enums;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.Core.Infrastuctures.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaseManager.WebAPI.Application.Repository
{
    public class MaintenanceRequestRepository : GenericRepository<MaintenanceRequest>, IMaintenanceRequestRepository
    {
        public MaintenanceRequestRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<MaintenanceRequest?> GetRequestWithDetailsAsync(int requestId)
        {
            return await _dbSet
                .Include(r => r.Property)
                .FirstOrDefaultAsync(r => r.Id == requestId);
        }

        public async Task<IEnumerable<MaintenanceRequest>> GetRequestsByPropertyIdAsync(int propertyId)
        {
            return await _dbSet
                .Where(r => r.PropertyId == propertyId)
                .ToListAsync();
        }

        public async Task<IEnumerable<MaintenanceRequest>> GetRequestsByStatusAsync(string status)
        {
            MaintenanceStatus maintenanceStatus;
            if (Enum.TryParse<MaintenanceStatus>(status, true, out maintenanceStatus))
            {
                return await _dbSet
                    .Where(r => r.Status == maintenanceStatus)
                    .ToListAsync();
            }
            return new List<MaintenanceRequest>();
        }
    }
}
