using LeaseManager.Core.Domain.Entities;

namespace LeaseManager.Core.Domain.Interfaces
{
    public interface IOwnerRepository : IGenericRepository<Owner>
    {
        Task<Owner?> GetOwnerWithPropertiesAsync(int ownerId);
 Task<bool> OwnerExistsByEmailAsync(string email);
    }
}