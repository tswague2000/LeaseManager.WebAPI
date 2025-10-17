using LeaseManager.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseManager.Core.Domain.Entities
{
    /// <summary>
    /// Représente un paiement lié à un bail.
    /// </summary>
    public class Payment
    {
        public int Id { get; set; }
        public int LeaseId { get; set; }
        public Lease Lease { get; set; }

        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public PaymentStatus Status { get; set; } = PaymentStatus.Completed;
        public PaymentMethod Method { get; set; } = PaymentMethod.BankTransfer;
    }
}
