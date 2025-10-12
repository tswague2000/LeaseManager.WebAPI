using LeaseManager.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaseManager.Core.Infrastuctures.Data.TypeConfigurations
{
    /// <summary>
    /// Configuration du type Payment pour Entity Framework Core.
    /// Définit la structure de la table, les contraintes et les relations.
    /// </summary>
    public class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            // Nom de la table
            builder.ToTable("Payments");

            #region Clé primaire
            builder.HasKey(p => p.Id);
            #endregion

            #region Propriétés
            // Montant payé
            builder.Property(p => p.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            // Date du paiement
            builder.Property(p => p.PaymentDate)
                   .IsRequired()
                   .HasColumnType("datetime2");

            // Statut du paiement (enum)
            builder.Property(p => p.Status)
                   .IsRequired()
                   .HasConversion<string>() // Stocke le nom de l’enum comme texte ("Paid", "Pending", etc.)
                   .HasMaxLength(50);
            #endregion

            #region Relations
            // Un paiement appartient à un bail
            builder.HasOne(p => p.Lease)
                   .WithMany(l => l.Payments)
                   .HasForeignKey(p => p.LeaseId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
