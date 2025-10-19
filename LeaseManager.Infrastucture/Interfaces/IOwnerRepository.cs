using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.FrameWork.Interface;

namespace LeaseManager.Infrastucture.Interfaces
{
    public interface IOwnerRepository : IGenericRepository<Owner>
    {
        Task<Owner?> GetOwnerWithPropertiesAsync(int ownerId);
        Task<bool> OwnerExistsByEmailAsync(string email);
    }
}
