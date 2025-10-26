using System;
using LeaseManager.Core.Domain.Interfaces;

namespace LeaseManager.Core.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDocumentRepository DocumentRepository { get; }
        ILeaseRepository LeaseRepository { get; }
        IPropertyRepository PropertyRepository { get; }
        IOwnerRepository OwnerRepository { get; }
        ITenantRepository TenantRepository { get; }
        IMaintenanceRequestRepository MaintenanceRequestRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IPropertyImageRepository PropertyImageRepository { get; }

        Task BeginTransactionAsync();
        Task<int> SaveChangesAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}