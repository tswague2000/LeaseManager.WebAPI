using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.Core.Infrastuctures.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaseManager.WebAPI.Application.Repository
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByLeaseIdAsync(int leaseId)
        {
            return await _dbSet
                .Where(p => p.LeaseId == leaseId)
                .ToListAsync();
        }

        public async Task<Payment?> GetPaymentWithDetailsAsync(int paymentId)
        {
            return await _dbSet
                .Include(p => p.Lease)
                .FirstOrDefaultAsync(p => p.Id == paymentId);
        }
    }
}
