using LeaseManager.Core.Domain.Enums;

namespace LeaseManager.WebAPI.Application.DTOs
{
    public class PaymentDTOs
    {
        public class PaymentReadDto
        {
            public int Id { get; set; }
            public int LeaseId { get; set; }
            public decimal Amount { get; set; }
            public DateTime PaymentDate { get; set; }
            public PaymentStatus Status { get; set; }
            public PaymentMethod Method { get; set; }
        }

        public class PaymentCreateDto
        {
            public int LeaseId { get; set; }
            public decimal Amount { get; set; }
            public DateTime PaymentDate { get; set; }
            public PaymentStatus Status { get; set; } = PaymentStatus.Completed;
            public PaymentMethod Method { get; set; } = PaymentMethod.BankTransfer;
        }

        public class PaymentUpdateDto
        {
            public decimal? Amount { get; set; }
            public DateTime? PaymentDate { get; set; }
            public PaymentStatus? Status { get; set; }
            public PaymentMethod? Method { get; set; }
        }
    }
}