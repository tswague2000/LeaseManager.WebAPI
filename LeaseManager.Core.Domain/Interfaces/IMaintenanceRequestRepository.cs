using LeaseManager.Core.Domain.Entities;

namespace LeaseManager.Core.Domain.Interfaces
{
 public interface IMaintenanceRequestRepository : IGenericRepository<MaintenanceRequest>
    {
 Task<MaintenanceRequest?> GetRequestWithDetailsAsync(int requestId);
        Task<IEnumerable<MaintenanceRequest>> GetRequestsByPropertyIdAsync(int propertyId);
 Task<IEnumerable<MaintenanceRequest>> GetRequestsByStatusAsync(string status);
    }
}