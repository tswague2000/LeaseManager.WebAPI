using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Infrastuctures.Data;
using LeaseManager.Infrastucture.Interfaces;

namespace LeaseManager.WebAPI.Application.Repository
{
    public class TenantRepository(AppDbContext context) : GenericRepository<Tenant>(context), ITenantRepository
    {
    }
}
