using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Infrastuctures.Data;
using LeaseManager.Infrastucture.Interfaces;

namespace LeaseManager.WebAPI.Application.Repository
{
    public class PaymentRepository(AppDbContext context) : GenericRepository<Payment>(context), IPaymentRepository
    {
    }
}
