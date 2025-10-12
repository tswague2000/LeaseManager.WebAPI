using LeaseManager.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaseManager.Core.Infrastuctures.Data.TypeConfigurations
{
    /// <summary>
    /// Configuration du type Lease pour Entity Framework Core.
    /// Définit les contraintes, les relations et les propriétés spécifiques à la table Lease.
    /// </summary>
    public class LeaseEntityTypeConfiguration : IEntityTypeConfiguration<Lease>
    {
        public void Configure(EntityTypeBuilder<Lease> builder)
        {
            // Nom de la table
            builder.ToTable("Leases");

            #region Clé primaire
            builder.HasKey(l => l.Id);
            #endregion

            #region Propriétés
            builder.Property(l => l.StartDate)
                   .IsRequired()
                   .HasColumnType("date");

            builder.Property(l => l.EndDate)
                   .IsRequired()
                   .HasColumnType("date");

            builder.Property(l => l.MonthlyRent)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
            #endregion

            #region Relations
            // Relation : un bail appartient à une propriété
            builder.HasOne(l => l.Property)
                   .WithMany(p => p.Leases)
                   .HasForeignKey(l => l.PropertyId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relation : un bail appartient à un locataire (User)
            builder.HasOne(l => l.Tenant)
                   .WithMany()
                   .HasForeignKey(l => l.TenantId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relation : un bail peut avoir plusieurs paiements
            builder.HasMany(l => l.Payments)
                   .WithOne(p => p.Lease)
                   .HasForeignKey(p => p.LeaseId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
