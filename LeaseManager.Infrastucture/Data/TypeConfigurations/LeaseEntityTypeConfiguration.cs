using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaseManager.Core.Infrastuctures.Data.TypeConfigurations
{
    /// <summary>
    /// Configuration du type Lease pour Entity Framework Core.
    /// Définit les contraintes, les relations et les propriétés spécifiques à la table Lease.
    /// </summary>
    public class LeaseConfiguration : IEntityTypeConfiguration<Lease>
    {
        public void Configure(EntityTypeBuilder<Lease> builder)
        {
            builder.ToTable("Leases");

            // Primary Key
            builder.HasKey(l => l.Id);

            // Properties
            builder.Property(l => l.StartDate)
                   .IsRequired();

            builder.Property(l => l.EndDate)
                   .IsRequired();

            builder.Property(l => l.MonthlyRent)
                   .IsRequired()
                   .HasColumnType("decimal(10,2)");

            builder.Property(l => l.Status)
                   .IsRequired()
                   .HasConversion<string>() // store enum as string
                   .HasMaxLength(50)
                   .HasDefaultValue(LeaseStatus.Active);

            // Relationships

            // Each Lease belongs to one Property
            builder.HasOne(l => l.Property)
                   .WithMany(p => p.Leases)
                   .HasForeignKey(l => l.PropertyId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Each Lease belongs to one Tenant
            builder.HasOne(l => l.Tenant)
                   .WithMany(t => t.Leases)
                   .HasForeignKey(l => l.TenantId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Each Lease can have many Payments
            builder.HasMany(l => l.Payments)
                   .WithOne(p => p.Lease)
                   .HasForeignKey(p => p.LeaseId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Each Lease can have many Documents
            builder.HasMany(l => l.Documents)
                   .WithOne(d => d.Lease)
                   .HasForeignKey(d => d.LeaseId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
