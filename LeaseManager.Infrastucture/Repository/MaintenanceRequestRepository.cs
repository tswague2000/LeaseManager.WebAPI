using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Infrastuctures.Data;
using LeaseManager.Infrastucture.Interfaces;

namespace LeaseManager.WebAPI.Application.Repository
{
    public class MaintenanceRequestRepository(AppDbContext context) : GenericRepository<MaintenanceRequest>(context), IMaintenanceRequestRepository
    {
    }
}
