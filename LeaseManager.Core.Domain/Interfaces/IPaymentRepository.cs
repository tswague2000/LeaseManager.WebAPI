using LeaseManager.Core.Domain.Entities;

namespace LeaseManager.Core.Domain.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetPaymentsByLeaseIdAsync(int leaseId);
        Task<Payment?> GetPaymentWithDetailsAsync(int paymentId);
    }
}