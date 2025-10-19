namespace LeaseManager.Core.Domain.Enums
{
    /// <summary>
    /// Statut d’un paiement lié à un bail.
    /// </summary>
    public enum PaymentStatus
    {
        Pending = 1,
        Completed = 2,
        Failed = 3
    }
}
