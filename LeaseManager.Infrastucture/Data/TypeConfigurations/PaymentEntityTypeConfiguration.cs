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
            builder.ToTable("Payments");

            // Primary Key
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.PaymentDate)
                   .IsRequired();

            builder.Property(p => p.Status)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(50);

            builder.Property(p => p.Method)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(50);

            // Relationships
            builder.HasOne(p => p.Lease)
                   .WithMany(l => l.Payments)
                   .HasForeignKey(p => p.LeaseId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}