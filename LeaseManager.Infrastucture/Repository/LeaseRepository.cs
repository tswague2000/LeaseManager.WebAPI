using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Infrastuctures.Data;
using LeaseManager.Infrastucture.Interfaces;

namespace LeaseManager.WebAPI.Application.Repository
{
    public class LeaseRepository(AppDbContext context) : GenericRepository<Lease>(context), ILeaseRepository
    {
    }
}
